using System.Collections.Generic;
using UnityEngine;


public class ShipController : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _movementInput
    //
    //   _pitchForce
    //   _rollForce
    //   _thrustForce
    //   _yawForce
    //
    //   _pitchAmount
    //   _rollAmount
    //   _yawAmount
    //
    //   _cockpitControls
    //   _engines
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [Header("Ship Input")]
    [SerializeField] private ShipMovementInput _movementInput;

    [Header("Ship Forces")]
    [SerializeField] [Range(1000f, 10000f)] private float _pitchForce   = 3000f;
    [SerializeField] [Range(1000f, 10000f)] private float _rollForce    = 1000f;
    [SerializeField] [Range(1000f, 10000f)] private float _yawForce     = 2000f;
    [SerializeField] [Range(1000f, 10000f)] private float _thrustForce  = 7500f;

    [Space(10f)]
    [SerializeField] [Range(-1f, 1f)]       private float _pitchAmount  = 0f;
    [SerializeField] [Range(-1f, 1f)]       private float _rollAmount   = 0f;
    [SerializeField] [Range(-1f, 1f)]       private float _yawAmount    = 0f;

    [Header("Ship Components")]
    [SerializeField] private AnimateCockpitControls       _cockpitControls;
    [SerializeField] private List<ShipEngine>             _engines;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _movementControls
    //   _rigidbody
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private IMovementControls _controlInput => _movementInput.MovementControls;
    private Rigidbody         _rigidbody;

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake() 
    //   FixedUpdate()
    //   Start()
    //   Update()
    // -------------------------------------------------------------------------

    #region .  Awake()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Awake()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }    // Awake()
    #endregion


    #region .  FixedUpdate()  .
    // -------------------------------------------------------------------------
    //   Method.......:  FixedUpdate()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidbody.AddTorque(transform.right * (_pitchAmount * _pitchForce * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, _rollAmount))
        {
            _rigidbody.AddTorque(transform.forward * (_rollAmount * _rollForce * Time.fixedDeltaTime));
        }

        if (!Mathf.Approximately(0f, _yawAmount))
        {
            _rigidbody.AddTorque(transform.up * (_yawAmount * _yawForce * Time.fixedDeltaTime));
        }

    }	 // FixedUpdate()
    #endregion


    #region .  Start()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Start()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Start()
    {
        foreach (ShipEngine engine in _engines)
        {
            engine.Initialize(_controlInput, _rigidbody, _thrustForce / _engines.Count);
        }

        _cockpitControls.Initialize(_controlInput);

    }    // Start()
    #endregion


    #region .  Update()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Update()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Update()
    {
        _pitchAmount = _controlInput.PitchAmount;
        _rollAmount  = _controlInput.RollAmount;
        _yawAmount   = _controlInput.YawAmount;

    }	 // Update()
    #endregion


}	// class ShipController
