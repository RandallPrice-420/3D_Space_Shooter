using UnityEngine;


public class Blaster : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Private Properties:
    // ------------------
    //   _canFire
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private bool _canFire
    {
        get
        {
            this._coolDown -= Time.deltaTime;

            return this._coolDown <= 0f;
        }
    }

    #endregion



    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _coolDownTime
    //   _muzzle
    //   _projectilePrefab
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField] [Range(0f, 5f)] private float      _coolDownTime = 0.25f;
    [SerializeField]                 private Transform  _muzzle;
    [SerializeField]                 private Projectile _projectilePrefab;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _coolDown
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private float _coolDown;

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   FireProjectile() 
    //   Update()
    // -------------------------------------------------------------------------

    #region .  FireProjectile()  .
    // -------------------------------------------------------------------------
    //   Method.......:  FireProjectile()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void FireProjectile()
    {
        this._coolDown = this._coolDownTime;

        Instantiate(this._projectilePrefab, this._muzzle.position, this.transform.rotation);

    }   // FireProjectile()

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
        if (this._canFire && Input.GetMouseButton(0))
        {
            this.FireProjectile();
        }

    }	 // Update()
    #endregion


}	// class Blaster
