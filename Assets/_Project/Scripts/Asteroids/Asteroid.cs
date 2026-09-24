using UnityEngine;


public class Asteroid : MonoBehaviour, IDamageable
{
    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _explosionPrefab
    //   _fracturedAsteroidPrefab
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField] private Detonator         _explosionPrefab;
    [SerializeField] private FracturedAsteroid _fracturedAsteroidPrefab;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _transform
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private Transform _transform;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   TakeDamage
    // -------------------------------------------------------------------------

    #region .  TakeDamage()  .
    // -------------------------------------------------------------------------
    //   Method.......:  TakeDamage()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void TakeDamage(int damage, Vector3 hitPosition)
    {
        FractureAsteroid(hitPosition);

    }   // TakeDamage()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
    //   FractureAsteroid()
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
        _transform = transform;

    }   // Awake()
    #endregion


    #region .  FractureAsteroid()  .
    // -------------------------------------------------------------------------
    //   Method.......:  FractureAsteroid()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void FractureAsteroid(Vector3 hitPosition)
    {
        if (_fracturedAsteroidPrefab != null)
        {
            Instantiate(_fracturedAsteroidPrefab, _transform.position, _transform.rotation);
        }

        if (_explosionPrefab != null)
        {
            Instantiate(_explosionPrefab, transform.position/*hitPosition*/, Quaternion.identity);
        }

        Destroy(gameObject);

    }   // FractureAsteroid()
    #endregion


}   // class Asteroid
