using UnityEngine;


public class DesktopMovementControls : MovementControlsBase
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   PitchAmount
    //   RollAmount
    //   ThrustAmount
    //   YawAmount
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public override float PitchAmount
    {
        get
        {
            Vector3 mousePosition = Input.mousePosition;
            float pitch           = (mousePosition.y - _screenCenter.y) / _screenCenter.y;

            return Mathf.Abs(pitch) > _deadZoneRadius ? pitch * -1f : 0f;
        }
    }

    public override float RollAmount
    {
        get
        {
            float roll;
            if (Input.GetKey(KeyCode.Q))
            {
                roll = 1f;
            }
            else
            {
                roll = Input.GetKey(KeyCode.E) ? -1f : 0f;
            }

            _rollAmount = Mathf.Lerp(_rollAmount, roll, Time.deltaTime * 3f);

            return _rollAmount;
        }
    }

    public override float ThrustAmount
    {
        get
        {
            return Input.GetAxis("Vertical");
        }
    }

    public override float YawAmount
    {
        get
        {
            Vector3 mousePosition = Input.mousePosition;
            float yaw             = (mousePosition.x - _screenCenter.x) / _screenCenter.x;

            return Mathf.Abs(yaw) > _deadZoneRadius ? yaw : 0f;
        }
    }

    #endregion



    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _deadZoneRadius
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField] private float _deadZoneRadius = 0.1f;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _rollAmount
    //   _screenCenter
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private float   _rollAmount   = 0f;
    private Vector2 _screenCenter = new(Screen.width / 2f, Screen.height / 2f);

    #endregion



}	// class DesktopMovementControls
