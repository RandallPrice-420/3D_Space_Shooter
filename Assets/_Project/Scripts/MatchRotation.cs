using UnityEngine;


public class MatchRotation : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _target
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField] private Transform _target;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _variable
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    //private float _variable = 0f;

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   LateUpdate()
    // -------------------------------------------------------------------------

    #region .  LateUpdate()  .
    // -------------------------------------------------------------------------
    //   Method.......:  LateUpdate()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void LateUpdate()
    {
        transform.rotation = _target.rotation;

    }	 // LateUpdate()
    #endregion


}	// class MatchRotation
