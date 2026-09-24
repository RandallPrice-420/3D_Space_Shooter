using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class AsteroidField : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   _radius
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public float Radius => _radius;

    #endregion



    // -------------------------------------------------------------------------
    // SerializeField Private Variables:
    // ---------------------------------
    //   _asteroidCount
    //   _maxScale
    //   _radius
    //   _asteroidPrefabs
    // -------------------------------------------------------------------------

    #region .  SerializeField Private Variables  .

    [SerializeField] [Range(100,  1000)]  private int      _asteroidCount = 500;
    [SerializeField] [Range(  1f,   10f)] private float    _maxScale      = 5f;
    [SerializeField] [Range(100f, 1000f)] private float    _radius        = 300f;
    [SerializeField]                      List<GameObject> _asteroidPrefabs;

    #endregion



    // -------------------------------------------------------------------------
    // Private Variables:
    // ------------------
    //   _transform
    // -------------------------------------------------------------------------

    #region .  Private Variables  .

    private Transform _transform;

    #endregion



    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
    //   OnEnable()
    //   SpawnAsteroids()
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
        _transform = transform;

    }   // Awake()
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
        SpawnAsteroids();

    }   // OnEnable()
    #endregion


    #region .  SpawnAsteroids()  .
    // -------------------------------------------------------------------------
    //   Method.......:  SpawnAsteroids()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void SpawnAsteroids()
    {
        for (int i = 0; i < _asteroidCount; ++i)
        {
            GameObject asteroid = Instantiate(_asteroidPrefabs[Random.Range(0, _asteroidPrefabs.Count)],
                                              _transform.position,
                                              Quaternion.identity);

            float scale = Random.Range(0.5f, _maxScale);

            asteroid.transform.localScale = new Vector3(scale, scale, scale);
            asteroid.transform.position  += Random.insideUnitSphere * _radius;

            asteroid.GetComponent<Rigidbody>()?.AddTorque(Random.insideUnitCircle * Random.Range(0f, 50f));
        }

    }   // SpawnAsteroids()
    #endregion


}   // class AsteroidField
