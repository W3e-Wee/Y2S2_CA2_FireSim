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

public class LevelManager : MonoBehaviour
{
    #region Variables
    public string currentLevelName;

    [Header("Scene Objects")]
    // Fires
    public List<FireObject> fireList;
    private Fire[] fires;

    // Walls
    public List<ConstructObject> wallList;
    private Construct[] walls;

    // Debris
    public List<DebrisObject> debrisList;
    private Debris[] debris;

    // Enemies
    private List<EnemyObject> enemyList;
    private EnemyStats[] enemies;

    #endregion

    // =======================
    // Private
    // =======================
    private PlayerCanvas playerCanvas;


    #region Unity Methods
    protected void Start()
    {
        // CheckGameStateChanged();
        playerCanvas = FindObjectOfType<PlayerCanvas>();

        // Get Objectives in scene
        fires = GameObject.FindObjectsOfType<Fire>();
        walls = GameObject.FindObjectsOfType<Construct>();
        debris = GameObject.FindObjectsOfType<Debris>();
        enemies = GameObject.FindObjectsOfType<EnemyStats>();

        // Populate Lists
        GetAllFiresInScene();
        GetAllDamagedWallsInScene();
        GetAllDebrisInScene();
        GetEnemiesInScene();
    }
    #endregion

    #region Level Methods

    /// <summary>
    /// Checks for the game's state and 
    /// fires events according to state
    /// </summary>
    private void CheckGameStateChanged()
    {
        // get current level name
        currentLevelName = SceneManager.GetActiveScene().name;

        // check to see if game is in a RUNNING state
        if (GameManager.Instance.CurrentGameState == GameManager.GameState.RUNNING)
        {
            // Fade out camera
            FadeCamera.Instance.FadeOutCanvas();

            // Disable menu canvas
            UIManager.Instance.SetMenuActive(false);

            // Play audio
            PlayLevelTheme(currentLevelName);

            return;
        }
    }

    /// <summary>
    /// Plays the theme of current level
    /// </summary>
    /// <param name="currentLevelName">The name of current loaded level</param>
    private void PlayLevelTheme(string currentLevelName)
    {
        // Check for audio to play
        switch (currentLevelName)
        {
            case "MainLevel_01_Scene":
                AudioManager.Instance.PlayMusic("Level 1 Theme");
                break;
            case "MainLevel_02_Scene":
                break;
            default:
                Debug.LogError("[LevelManager] - Audio for " + currentLevelName + " not found");
                break;
        }
    }

    #endregion

    # region Fire Methods

    /// <summary>
    /// Adds each fire in the scene to a List of FireObject
    /// </summary>
    private void GetAllFiresInScene()
    {
        if (fires.Length == 0)
        {
            Debug.LogWarning("[LevelManager] - No Fires in scene");
            return;
        }

        foreach (Fire f in fires)
        {
            try
            {
                // create a FireObject class
                FireObject fireObj = new FireObject();

                // populate data
                fireObj.fireId = f.fireId;
                fireObj.isExtinguished = f.extinguished;
                fireObj.firePrefab = f.gameObject;
                fireObj.firePosition = f.transform.position;

                // add to list
                fireList.Add(fireObj);
            }
            catch (System.Exception e)
            {
                Debug.LogError("[LevelManager] - An error occured when populating Fire List: " + e.Message);
            }
        }

        return;
    }

    /// <summary>
    /// Updates the FireObject's isExtinguished state
    /// </summary>
    /// <param name="fireId">The Id of the fire</param>
    /// <param name="fireState">The state of the fire</param>
    public void UpdateFireState(string fireId, bool fireState)
    {
        foreach (FireObject f in fireList)
        {
            if (f.fireId == fireId)
            {
                // update the fire's state
                f.isExtinguished = fireState;
                return;
            }
        }
    }

    #endregion

    #region Wall Methods

    /// <summary>
    /// Adds each wall in the scene to a List of ConstructObject
    /// </summary>
    private void GetAllDamagedWallsInScene()
    {
        if (walls.Length == 0)
        {
            Debug.LogWarning("[LevelManager] - No Constructs in scene");
            return;
        }

        foreach (Construct c in walls)
        {
            try
            {
                // Create a nre ConstructObject
                ConstructObject wallObj = new ConstructObject();

                // populate with data
                wallObj.wallId = c.wallId;
                wallObj.wallPosition = c.transform.position;
                wallObj.wallPrefab = c.gameObject;
                wallObj.isFixed = c.repaired;

                // Add to list
                wallList.Add(wallObj);
            }
            catch (System.Exception e)
            {
                Debug.LogError("[LevelManager] - An error occured when populating Wall List: " + e.Message);
            }
        }

        return;
    }

    /// <summary>
    /// Updates the ConstructObject's isFixed state
    /// </summary>
    /// <param name="wallId">The unqiue ID of the wall</param>
    /// <param name="wallState">The current state of the wall</param>
    public void UpdateWallState(string wallId, bool wallState)
    {
        foreach (ConstructObject wall in wallList)
        {
            if (wall.wallId == wallId)
            {
                // update the wall's state
                wall.isFixed = wallState;
                return;
            }
        }
    }

    #endregion

    #region Debris Methods

    /// <summary>
    /// Adds each debris in the scene to a List of DebrisObject
    /// </summary>
    private void GetAllDebrisInScene()
    {
        if (debris.Length == 0)
        {
            Debug.LogWarning("[LevelManager] - No Debris in scene");
            return;
        }

        foreach (Debris d in debris)
        {
            try
            {
                // Create new DebrisObject
                DebrisObject debrisObj = new DebrisObject();

                // populate with data
                debrisObj.debrisId = d.debrisId;
                debrisObj.debrisPosition = d.transform.position;
                debrisObj.debrisPrefab = d.gameObject;
                debrisObj.isCleared = d.cleared;

                // Add to list
                debrisList.Add(debrisObj);
            }
            catch (System.Exception e)
            {
                Debug.LogError("[LevelManager] - An error occured when populateing Debris List: " + e.Message);
            }
        }

        return;
    }

    /// <summary>
    /// Updates the DebrisObject's isCleared state
    /// </summary>
    /// <param name="debrisId">The GUID of the debris</param>
    /// <param name="debrisState">The current state of the debri</param>
    public void UpdateDebrisState(string debrisId, bool debrisState)
    {
        foreach (DebrisObject d in debrisList)
        {
            if (d.debrisId == debrisId)
            {
                // update the wall's state
                d.isCleared = debrisState;

                return;
            }
        }
    }

    #endregion

    #region Enemy Methods
    
    /// <summary>
    /// Adds each enemy in the scene to a List of EnemyObject
    /// </summary>
    private void GetEnemiesInScene()
    {
        if (enemies.Length == 0)
        {
            Debug.LogWarning("[LevelManager] - No Enemies in scene");
            return;
        }

        foreach (EnemyStats enemy in enemies)
        {
            try
            {
                // Create a new EnemyObject
                EnemyObject enemyObj = new EnemyObject();

                // Populate with data
                enemyObj.enemyId = enemy.enemyId;
                enemyObj.spawnPosition = enemy.transform.position;
                enemyObj.enemyPrefab = enemy.gameObject;
                enemyObj.isDead = enemy.isDead;

                // Add to list
                enemyList.Add(enemyObj);
            }
            catch(System.Exception e)
            {
                Debug.LogError("[LevelManager] - An error occured when populating Enemy List: " + e.Message);
            }
        }

        return;
    }

    /// <summary>
    /// Updates the EnemyObject's isDead state
    /// </summary>
    /// <param name="enemyId">The enemy's GUID</param>
    /// <param name="enemyState">The current state of the enemy</param>
    public void UpdateEnemyState(string enemyId, bool enemyState)
    {
        foreach(EnemyObject enemy in enemyList)
        {
            if(enemy.enemyId == enemyId)
            {
                enemy.isDead = enemyState;
                return;
            }
        }
    }
    
    #endregion

    #region Canvas Methods

    /// <summary>
    /// Toggles on or off Game Over Canvas
    /// </summary>
    public void ToggleGameOver()
    {
        playerCanvas.ShowGameOver(currentLevelName);
    }

    #endregion
}