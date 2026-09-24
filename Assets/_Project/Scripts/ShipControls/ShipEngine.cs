using UnityEngine;


public class ShipEngine : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _isThrustersEnabled
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private bool _isThrustersEnabled => !Mathf.Approximately(0f, _shipMovementControls.ThrustAmount);

    #endregion



    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _thruster
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField] private GameObject _thruster;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _rigidbody
    //   _shipMovementControls
    //   _thrustAmount
    //   _thrustForce
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private Rigidbody         _rigidbody;
    private IMovementControls _shipMovementControls;
    private float             _thrustAmount = 0f;
    private float             _thrustForce;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   Initialize()
    // -------------------------------------------------------------------------

    #region .  Initialize()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Initialize()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void Initialize(IMovementControls movementControls, Rigidbody rb, float thrustForce)
    {
        _rigidbody            = rb;
        _shipMovementControls = movementControls;
        _thrustForce          = thrustForce;

    }   // Initialize()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   ActivateThrusters()
    //   FixedUpdate() 
    //   Update()
    // -------------------------------------------------------------------------

    #region .  ActivateThrusters()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ActivateThrusters()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void ActivateThrusters()
    {
        _thruster.SetActive(_isThrustersEnabled);

        if (!_isThrustersEnabled) return;

        _thrustAmount = _thrustForce * _shipMovementControls.ThrustAmount;

    }   // ActivateThrusters()
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
        if (!_isThrustersEnabled) return;

        _rigidbody.AddForce(_thrustAmount * Time.fixedDeltaTime * transform.forward);

    }    // FixedUpdate()
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
        ActivateThrusters();

    }    // Update()
    #endregion


}	// class ShipEngine
