using System;
using UnityEngine;


public class ShipInputManager : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Enums:
    // -------------
    //   InputType
    // -------------------------------------------------------------------------

    #region .  Public Enums  .

    public enum InputType
    {
        HumanDesktop,
        HumanMobile,
        Bot
    }

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   GetInputControls()
    // -------------------------------------------------------------------------

    #region .  GetInputControls()  .
    // -------------------------------------------------------------------------
    //   Method.......:  GetInputControls()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static IMovementControls GetInputControls(InputType inputType)
    {
        return inputType switch
        {
            InputType.HumanDesktop => new DesktopMovementControls(),
            InputType.HumanMobile  => null,
            InputType.Bot          => null,
            _                      => throw new ArgumentOutOfRangeException("Invalid input type: " + inputType)
        };

    }   // GetInputControls()
    #endregion


}	// class ShipInputManager
