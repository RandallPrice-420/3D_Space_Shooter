using UnityEngine;


public class Managers : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _instance
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private static Managers _instance;

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
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
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }	 // Awake()
    #endregion


}	// class Managers
