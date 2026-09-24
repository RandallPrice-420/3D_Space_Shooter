using UnityEngine;


public class FracturedAsteroid : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _duration
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField][Range(1f, 60f)] private float _duration = 10f;

    #endregion



    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   OnEnable
    // -------------------------------------------------------------------------

    #region .  OnEnable()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnEnable()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------

    private void OnEnable()
    {
        Destroy(gameObject, _duration);

    }   // OnEnable()
    #endregion


}   // class FracturedAsteroid
