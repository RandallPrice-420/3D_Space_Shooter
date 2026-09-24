using System.Collections.Generic;
using UnityEngine;


public class AnimateCockpitControls : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _joystick
    //   _joystickRange
    //   _throttles
    //   _throttleRange
    //   _movementInput
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [Header("Flight control transforms and ranges")]
    [SerializeField] private Transform         _joystick;
    [SerializeField] private Vector3           _joystickRange = Vector3.zero;
    [SerializeField] private List<Transform>   _throttles;
    [SerializeField] private float             _throttleRange = 35f;
    [SerializeField] private ShipMovementInput _movementInput;

    #endregion



    // -------------------------------------------------------------------------
    // Private Properties:
    // ------------------
    //   _movementControls
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private IMovementControls _movementControls;

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
    public void Initialize(IMovementControls movementControls)
    {
        _movementControls = movementControls;

    }	 // Initialize()
    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Update()
    // -------------------------------------------------------------------------

    #region .  Update()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Update()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Update()
    {
        if (_movementControls == null)
        {
            return;
        }

        _joystick.localRotation = Quaternion.Euler(_joystickRange.x * _movementControls.PitchAmount,
                                                   _joystickRange.y * _movementControls.YawAmount,
                                                   _joystickRange.z * _movementControls.RollAmount);

        Vector3 throttleRotation = _throttles[0].localRotation.eulerAngles;
        throttleRotation.x       = _movementControls.ThrustAmount * _throttleRange;

        foreach (Transform throttle in _throttles)
        {
            throttle.localRotation = Quaternion.Euler(throttleRotation);
        }

    }	 // Update()
    #endregion


}	// class AnimateCockpitControls
