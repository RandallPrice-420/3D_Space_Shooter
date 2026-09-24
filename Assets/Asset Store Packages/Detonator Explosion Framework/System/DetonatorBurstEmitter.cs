using UnityEngine;


/*
	DetonatorBurstEmitter is an interface for DetonatorComponents to use to create particles
	
	- Handles common tasks for Detonator... almost every DetonatorComponent uses this for particles
	- Builds the gameobject with emitter, animator, renderer
	- Everything incoming is automatically scaled by size, timeScale, color
	- Enable oneShot functionality

	You probably don't want to use this directly... though you certainly can.
*/

public class DetonatorBurstEmitter : DetonatorComponent
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // -----------------
    //   SizeOverLifetimeCurve
    //
    //   angularVelocity
    //   colorAnimation
    //   count
    //   damping
    //   durationVariation
    //   explodeOnAwake
    //   exponentialGrowth
    //   maxScreenSize
    //   oneShot
    //   particleSize
    //   randomRotation
    //   sizeGrow
    //   sizeVariation
    //   startRadius
    //   useExplicitColorAnimation
    //   useExplicitSizeCurve
    //   useWorldSpace
    //   upwardsBias
    //   material
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public AnimationCurve SizeOverLifetimeCurve = new AnimationCurve();

	public float    angularVelocity           = 20f;
	public Color[]  colorAnimation            = new Color[5];
	public float    count                     = 1;
	public float    damping                   = 1f;
	public float    durationVariation         = 0f;
	public bool     explodeOnAwake            = false;
	public bool     exponentialGrowth         = true;
	public float    maxScreenSize             = 2f;
	public bool     oneShot                   = true;
	public float    particleSize              = 1f;
	public bool     randomRotation            = true;
	public float    sizeGrow                  = 20f;
	public float    sizeVariation             = 0f;
	public float    startRadius               = 1f;
	public bool     useExplicitColorAnimation = false;
	public bool     useExplicitSizeCurve      = false;  // If left as false, SizeOverLifetieme will be set to a linear line
    public bool     useWorldSpace             = true;
	public float    upwardsBias               = 0f;
	public Material material;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _particleSystem
    //   _psColorOverLifetime
    //   _psCoLGradient 
    //   _psCoLGradientAlpha
    //   _psCoLGradientColor
    //   _psEmission
    //   _psEmitParams
    //   _psMain
    //   _psRenderer
    //   _psShape
    //   _psSizeOverLifetime
    //   _psSoLMMCurve
    //   _psVelocityOverLifetime
    //
    //   _baseDamping             
    //   _baseSize                
    //   _baseColor               
    //   _delayedExplosionStarted 
    //   _emitTime
    //   _explodeDelay
    //   initFraction
    //   _randomizedRotation
    //   _scaledColor
    //   _scaledDuration
    //   _scaledDurationVariation
    //   _scaledStartRadius
    //   speed
    //   _thisPos
    //   _tmpAngularVelocity
    //   _tmpCount
    //   _tmpDir
    //   _tmpDuration
    //   _tmpParticleSize
    //   _tmpPos
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private ParticleSystem                                 _particleSystem;
	private ParticleSystem.ColorOverLifetimeModule         _psColorOverLifetime;
	private Gradient                                       _psCoLGradient = new Gradient();
	private GradientAlphaKey[]                             _psCoLGradientAlpha;
	private GradientColorKey[]                             _psCoLGradientColor;
	private ParticleSystem.EmissionModule                  _psEmission;
	private ParticleSystem.EmitParams                      _psEmitParams;
	private ParticleSystem.MainModule                      _psMain;
	private ParticleSystemRenderer                         _psRenderer;
	private ParticleSystem.ShapeModule                     _psShape;
	private ParticleSystem.SizeOverLifetimeModule          _psSizeOverLifetime;
	private ParticleSystem.MinMaxCurve                     _psSoLMMCurve;
	private ParticleSystem.LimitVelocityOverLifetimeModule _psVelocityOverLifetime;

	private float   _baseDamping             = 0.1300004f;
	private float   _baseSize                = 1f;
	private Color   _baseColor               = Color.white;
	private bool    _delayedExplosionStarted = false;
	private float   _emitTime;
	private float   _explodeDelay;
	private float   initFraction             = 0.1f;
	private float   _randomizedRotation;
	private float   _scaledColor;				// Color with alpha adjusted according to detail and duration
	private float   _scaledDuration;			// Calculated duration... duration * timescale
	private float   _scaledDurationVariation; 
	private float   _scaledStartRadius; 
	private float   speed                    = 3.0f;
	private Vector3 _thisPos;					// Handle on this gameobject's position, set inside
	private float   _tmpAngularVelocity;		// Random angular velocity from -angularVelocity to +angularVelocity, if randomRotation is true;
	private float   _tmpCount;					// Calculated count... incoming count * incoming detail
	private Vector3 _tmpDir;					// Calculated velocity - randomized inside sphere - incoming velocity * size
	private float   _tmpDuration;				// Calculated duration... incoming duration * incoming timescale
    private float   _tmpParticleSize;			// Calculated particle size... particleSize * randomized size (by sizeVariation)
	private Vector3 _tmpPos;					// Calculated position... randomized inside sphere of incoming radius * size

	// Assiagned but not used, so I commented these out to prevent a warning in the Error List.
	//ParticleSystemRenderMode _psRenderMode = ParticleSystemRenderMode.Billboard;
    //static float epsilon = 0.01f;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
	//   Explode()
    //   Init()  --  UNUSED
	//   Reset()
    // -------------------------------------------------------------------------

    #region .  Explode()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Explode()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    override public void Explode()
    {
		if (on)
		{			
			if (useWorldSpace)
				_psMain.simulationSpace = ParticleSystemSimulationSpace.World; 
			else			
				_psMain.simulationSpace = ParticleSystemSimulationSpace.Local; 
			
			_scaledDuration          = timeScale * duration;
			_scaledDurationVariation = timeScale * durationVariation;
			_scaledStartRadius       = size * startRadius;
			
			if (!_delayedExplosionStarted)
			{
				_explodeDelay = explodeDelayMin + (Random.value * (explodeDelayMax - explodeDelayMin));
			}
			if (_explodeDelay <= 0) 
			{				
				if (useExplicitColorAnimation)
				{
					float timeDivision  = 1.0f / colorAnimation.Length;
					_psCoLGradientColor = new GradientColorKey[colorAnimation.Length]; 
					_psCoLGradientAlpha = new GradientAlphaKey[colorAnimation.Length]; 

					for (int i = 0; i < colorAnimation.Length; i++)
					{
						_psCoLGradientColor[i] = new GradientColorKey(colorAnimation[i],   (timeDivision * i) + timeDivision);
						_psCoLGradientAlpha[i] = new GradientAlphaKey(colorAnimation[i].a, (timeDivision * i) + timeDivision);
					}

					_psCoLGradient.SetKeys(_psCoLGradientColor, _psCoLGradientAlpha);
					_psColorOverLifetime.color = _psCoLGradient;
				}
				else //auto fade
				{
					float timeDivision  = 1 / colorAnimation.Length;
					_psCoLGradientColor = new GradientColorKey[colorAnimation.Length]; 
					_psCoLGradientAlpha = new GradientAlphaKey[colorAnimation.Length]; 

					for (int i = 0; i < colorAnimation.Length; i++)
					{
						_psCoLGradientColor[i] = new GradientColorKey(color, timeDivision * i);
					}
					_psCoLGradientAlpha = new GradientAlphaKey[] {new GradientAlphaKey(color.a * .7f, timeDivision * 0), 
																  new GradientAlphaKey(color.a *  1f, timeDivision * 1), 
																  new GradientAlphaKey(color.a * .5f, timeDivision * 2), 
																  new GradientAlphaKey(color.a * .3f, timeDivision * 3), 
																  new GradientAlphaKey(color.a *  0f, timeDivision * 4), };

					_psCoLGradient.SetKeys(_psCoLGradientColor, _psCoLGradientAlpha);
					_psColorOverLifetime.color = _psCoLGradient;
				}
				
				_tmpCount = count * detail;
				if (_tmpCount < 1) _tmpCount = 1;

				if (useWorldSpace)
				{
					_thisPos = this.gameObject.transform.position;
				}
				else
				{
					_thisPos = new Vector3(0, 0, 0);
				}

				for (int i = 1; i <= _tmpCount; i++)
				{
					_tmpPos   =  Vector3.Scale(Random.insideUnitSphere, new Vector3(_scaledStartRadius, _scaledStartRadius, _scaledStartRadius)); 
					_tmpPos   = _thisPos + _tmpPos;
									
					_tmpDir   = Vector3.Scale(Random.insideUnitSphere, new Vector3(velocity.x, velocity.y, velocity.z)); 
					_tmpDir.y = (_tmpDir.y + (2 * (Mathf.Abs(_tmpDir.y) * upwardsBias)));
					
					if (randomRotation == true)
					{
						_randomizedRotation = Random.Range(-1f,1f);
						_tmpAngularVelocity = Random.Range(-1f,1f) * angularVelocity;
						
					}
					else
					{
						_randomizedRotation = 0f;
						_tmpAngularVelocity = angularVelocity;
					}
					
					_tmpDir = Vector3.Scale(_tmpDir, new Vector3(size, size, size));
					
					 _tmpParticleSize = size * (particleSize + (Random.value * sizeVariation));
					
					_tmpDuration = _scaledDuration + (Random.value * _scaledDurationVariation);

					_psEmitParams.startColor      = color;
					_psEmitParams.rotation        = _randomizedRotation;
					_psEmitParams.angularVelocity = _tmpAngularVelocity;
					_psEmitParams.velocity        = _tmpDir;
					_psEmitParams.startLifetime   = _tmpDuration;
					_psEmitParams.startSize       = _tmpParticleSize;
					_psEmitParams.position        = _tmpPos;

					_particleSystem.Emit(_psEmitParams, 1);
				}

				_delayedExplosionStarted        = false;
				_emitTime                       = Time.time;
				_explodeDelay                   = 0f;

				_psMain.startLifetime           = _tmpDuration;
				_psRenderer.material            = material;

				// End, so emit.
				_psEmission            .enabled = true;
				_psColorOverLifetime   .enabled = true;
				_psSizeOverLifetime    .enabled = true;
				_psVelocityOverLifetime.enabled = true;

				_psEmission.enabled             = false;
			}
			else
			{
				// Tell Update() to start reducing the start delay and call Explode() again when it's zero.
				_delayedExplosionStarted = true;
			}
		}

    }	// Explode()
    #endregion


    #region .  Init()  --  UNUSED  .
    // -------------------------------------------------------------------------
    //   Method.......:  Init()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    override public void Init() 
	{
		print ("UNUSED");

	}   // Init()
    #endregion


    #region .  Reset()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Reset()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Reset()
    {
		color   = _baseColor;
		damping = _baseDamping;
		size    = _baseSize;

    }   // Reset()
	#endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
    //   OnCollisionEnter()
    //   OnEnable()
    //   Update()
    // -------------------------------------------------------------------------

    #region .  Awake()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Awake()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Awake()
    {
		_particleSystem = (gameObject.AddComponent<ParticleSystem>()) as ParticleSystem;

		if (gameObject.GetComponent<ParticleSystemRenderer>())
		{
			_psRenderer = gameObject.GetComponent<ParticleSystemRenderer>();
		}
		else
		{
			_psRenderer = (gameObject.AddComponent<ParticleSystemRenderer>()) as ParticleSystemRenderer;
		}

		_particleSystem.hideFlags            = HideFlags.HideAndDontSave;

		_psColorOverLifetime                 = _particleSystem.colorOverLifetime;
		_psEmission                          = _particleSystem.emission;
		_psEmitParams                        = new ParticleSystem.EmitParams();
		_psMain                              = _particleSystem.main;
		_psShape                             = _particleSystem.shape;
		_psSizeOverLifetime                  = _particleSystem.sizeOverLifetime;
		_psVelocityOverLifetime              = _particleSystem.limitVelocityOverLifetime;

		_psEmission.enabled                  = false;
		_psEmission.rateOverTime             = 0;
		_psEmission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0.0f, 1, 1)});   

		_psMain.loop                         = false;
		_psMain.flipRotation                 = .5f;
		_psMain.startSpeed                   = 0f;

		_psRenderer.material                 = material;
		_psRenderer.material.color           = Color.white;
		_psRenderer.maxParticleSize          = maxScreenSize;
		_psRenderer.receiveShadows           = true;
		_psRenderer.shadowCastingMode        = UnityEngine.Rendering.ShadowCastingMode.On;

		_psShape.enabled                     = true;
		_psShape.radius                      = 0.1f;
		_psShape.shapeType                   = ParticleSystemShapeType.Sphere;

		//Defaulting to a linear curve
		if (!useExplicitSizeCurve)
		{
			SizeOverLifetimeCurve.AddKey(0.0f, 0.1f);
			SizeOverLifetimeCurve.AddKey(1.0f, 1.0f);
		}	

		_psSoLMMCurve                        = new ParticleSystem.MinMaxCurve(2, SizeOverLifetimeCurve);

		_psSizeOverLifetime.size             = _psSoLMMCurve;

		_psVelocityOverLifetime.dampen       = _baseDamping;
		_psVelocityOverLifetime.enabled      = true;
		_psVelocityOverLifetime.limitX       = .1f;
		_psVelocityOverLifetime.limitY       = .1f;
		_psVelocityOverLifetime.limitZ       = .1f;
		_psVelocityOverLifetime.space        = ParticleSystemSimulationSpace.World;
		_psVelocityOverLifetime.separateAxes = true;
		
		if (explodeOnAwake)
		{
			Explode();
		}

    }   // Awake()
    #endregion


    #region .  SizeFunction()  .
    // -------------------------------------------------------------------------
    //   Method.......:  SizeFunction()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private float SizeFunction (float elapsedTime) 
	{
		float divided = 1 - (1 / (1 + elapsedTime * speed));
		return initFraction + (1 - initFraction) * divided;

    }	// SizeFunction()
    #endregion


    #region .  Update()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Update()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Update () 
	{		
		// Delayed explosion.
		if (_delayedExplosionStarted)
		{
			_explodeDelay = (_explodeDelay - Time.deltaTime);
			if (_explodeDelay <= 0f)
			{
				Explode();
			}
		}

	}   // Update()
    #endregion


}	// class DetonatorBurstEmitter
