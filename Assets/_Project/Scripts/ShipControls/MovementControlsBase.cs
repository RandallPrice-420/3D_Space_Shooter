public abstract class MovementControlsBase : IMovementControls
{
    // -------------------------------------------------------------------------
    // Public Variables:
    // ------------------
    //   PitchAmount
    //   RollAmount
    //   ThrustAmount
    //   YawAmount
    // -------------------------------------------------------------------------

    #region .  Public Variables  .

    public abstract float PitchAmount  { get; }

    public abstract float RollAmount   { get; }

    public abstract float ThrustAmount { get; }

    public abstract float YawAmount    { get; }


    #endregion


}	// class MovementControlsBase
