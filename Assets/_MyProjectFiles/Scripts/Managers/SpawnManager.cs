using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: Determines where gameObjects spawn
//---------------------------------------------------------------------------------

public class SpawnManager : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================

    //====================================
    // [SerializeField] Private Variables
    //====================================
	[Header("Spawn Settings")]
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnDelay = 0f;

    //===================
    // Private Variables
    //===================
    private Vector3 spawnLocation;

    #endregion

    #region Unity Methods
    protected void Start()
    {
		Invoke("Spawn", spawnDelay);
    }
    #endregion

    #region Own Methods
    private void Spawn()
    {
        // for each spawnpoint spawn in 1 gameObject at transform
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            spawnLocation = spawnPoints[i].position;
            Instantiate(spawnPrefab, spawnLocation, Quaternion.identity);
        }
    }
    #endregion

}
