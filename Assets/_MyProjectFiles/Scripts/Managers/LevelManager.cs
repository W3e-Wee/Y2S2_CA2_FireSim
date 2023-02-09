using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-08
// Description	: Script to manage level logic
//---------------------------------------------------------------------------------
// Responsible for:
// 1. Spawning required prefabs
// 2. Keep track of win/lose
// 3. Interact with other managers (i.e. UIManager, GameManager)
//---------------------------------------------------------------------------------

[System.Serializable]
public class FireSpawns
{
    public GameObject firePrefab;
    public Vector3 spawnPoint;
    public bool isExtinguished;
}

[System.Serializable]
public class Walls
{
    public GameObject wallPrefab;
    public bool isFixed;
}

public class LevelManager : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================
    public string currentLevelName;
    [Header("Level Objects")]
    public FireSpawns[] fires;
    public Walls[] damageWalls;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        // display current scene on start
        Scene currentScene = SceneManager.GetActiveScene();
        currentLevelName = currentScene.name;

        // get all fire and walls in scene
        // fires = GameObject.FindObjectsOfType<Fire>();
        // damageWalls = GameObject.FindObjectsOfType<Construct>(); 
    }

    #endregion

    #region Own Methods
	private void SetUpFire()
	{
		if(fires.Length == 0)
		{
			Debug.LogWarning("[LevelManager] - No Fires in " + currentLevelName);
			return;
		}

		foreach(FireSpawns f in fires)
		{
			
		}
	}
    #endregion

}
