using UnityEngine;


public class GameManager : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _instance
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private bool ShouldQuitGame => Input.GetKeyUp(KeyCode.Escape);

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   QuitGame()
    //   Start()
    //   Update()
    // -------------------------------------------------------------------------

    #region .  QuitGame()  .
    // -------------------------------------------------------------------------
    //   Method.......:  QuitGame()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif

    }    // QuitGame()
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
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible   = false;

    }	 // Start()
    #endregion


    #region .  Update()Awake  .
    // -------------------------------------------------------------------------
    //   Method.......:  Update()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void Update()
    {
        if (ShouldQuitGame)
        {
            QuitGame();
        }

    }	 // Update()
    #endregion


}	// class GameManager
