using UnityEngine;


public class ShipMovementInput : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // -----------------
    //   MovementControls
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public IMovementControls MovementControls { get; private set; }

    #endregion



    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _inputType
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField] private ShipInputManager.InputType _inputType = ShipInputManager.InputType.HumanDesktop;

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   OnDestroy()
    //   Start()
    // -------------------------------------------------------------------------

    #region .  OnDestroy()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnDestroy()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnDestroy()
    {
        MovementControls = null;

    }	 // OnDestroy()
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
        MovementControls = ShipInputManager.GetInputControls(_inputType);

    }	 // Start()
    #endregion


}	// class ShipMovementInput
