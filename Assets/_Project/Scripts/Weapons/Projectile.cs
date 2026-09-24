using UnityEngine;


public class Projectile : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Private Properties:
    // ------------------
    //   _isOutOfFuel
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private bool _isOutOfFuel
    {
        get 
        {
            _duration -= Time.deltaTime;

            return _duration <= 0f;
        }
    }

    #endregion



    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _damage
    //   _launchForce
    //   _range
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField] [Range(10f, 1000f)]    private int   _damage      =   100;
    [SerializeField] [Range(5000f, 25000f)] private float _launchForce = 10000f;
    [SerializeField] [Range(2f, 10f)]       private float _range       =     2f;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _duration
    //   _rigidbody
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private float     _duration;
    private Rigidbody _rigidbody;

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
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();

    }	 // Awake()
    #endregion


    #region .  OnCollisionEnter()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnCollisionEnter()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Projectile collided with {collision.collider.name}");

        if (collision.collider.gameObject.TryGetComponent<IDamageable>(out var damagable))
        {
            Vector3 hitPosition = collision.GetContact(0).point;
            damagable.TakeDamage(_damage, hitPosition);
        }

    }	 // OnCollisionEnter()
    #endregion


    #region .  OnEnable()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnEnable()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnEnable()
    {
        _rigidbody.AddForce(_launchForce * transform.forward);
        _duration = _range;

    }	 // OnEnable()
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
        if (_isOutOfFuel)
        {
            Destroy(gameObject);
        }

    }	 // Update()
    #endregion


}	// class Projectile
