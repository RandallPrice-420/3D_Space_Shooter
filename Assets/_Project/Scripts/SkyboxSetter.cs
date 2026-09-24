using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Skybox))]
public class SkyboxSetter : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _skyboxIndex
    //   _skyboxMaterials
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField] private int            _skyboxIndex = 0;
    [SerializeField] private List<Material> _skyboxMaterials;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _skybox
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private Skybox _skybox;

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
    //   ChangeSkybox()
    //   ChangeSkyboxRandom()
    //   OnEnable()
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
        _skybox = GetComponent<Skybox>();

    }	 // Awake()
    #endregion


    #region .  ChangeSkybox()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ChangeSkybox()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void ChangeSkybox(int skyboxIndex)
    {
        if ((_skybox    != null) && 
            (skyboxIndex >= 0)   &&
            (skyboxIndex < _skyboxMaterials.Count))
        {
            _skybox.material = _skyboxMaterials[skyboxIndex];
        }

    }   // ChangeSkybox()
    #endregion


    #region .  ChangeSkyboxRandom()  .
    // -------------------------------------------------------------------------
    //   Method.......:  ChangeSkyboxRandom()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void ChangeSkyboxRandom()
    {
        if (_skyboxMaterials.Count > 0)
        {
            int randomIndex  = Random.Range(0, _skyboxMaterials.Count);
            _skybox.material = _skyboxMaterials[randomIndex];
        }

    }   // ChangeSkyboxRandom()
    #endregion


    #region .  OnEnable()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnEnable()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnEnable()
    {
        ChangeSkybox(_skyboxIndex);

    }   // OnEnable()
    #endregion


}	// class SkyboxSetter
