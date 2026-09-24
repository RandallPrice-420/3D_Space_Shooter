using System.Collections.Generic;
using UnityEngine;


public class CameraManager : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Enums:
    // -------------
    //   VirtualCameras
    // -------------------------------------------------------------------------

    #region .  Public Enums  .

    public enum VirtualCameras
    {
        NoCamera      = -1,
        CockpitCamera =  0,
        FollowCamera  =  1
    }

    #endregion



    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _cameraKeyPressed
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private VirtualCameras _cameraKeyPressed
    {
        get
        {
            for (int i = 0; i < _virtualCameras.Count; ++i)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1 + i))
                {
                    return (VirtualCameras)i;
                }
            }

            return VirtualCameras.NoCamera;
        }
    }

    #endregion



    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _virtualCameras
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [Header("Virtual Cameras")]
    [SerializeField] private List<GameObject> _virtualCameras;

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   SetActiveCamera()
    //   Start() 
    // -------------------------------------------------------------------------

    #region .  SetActiveCamera()  .
    // -------------------------------------------------------------------------
    //   Method.......:  SetActiveCamera()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void SetActiveCamera(VirtualCameras activeCamera)
    {
        if (activeCamera == VirtualCameras.NoCamera)
        {
            //Debug.Log("No camera");
            return;
        }

        Debug.Log($"activeCamera: {activeCamera}");

        foreach (GameObject camera in _virtualCameras)
        {
            camera.SetActive(camera.CompareTag(activeCamera.ToString()));
        }

    }   // SetActiveCamera()
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
        SetActiveCamera(VirtualCameras.CockpitCamera);

    }    // Start()
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
        SetActiveCamera(_cameraKeyPressed);

    }	 // Update()
    #endregion


}	// class CameraManager
