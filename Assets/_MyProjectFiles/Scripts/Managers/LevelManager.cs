using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using TMPro;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-08
// Description	: Script to manage level logic
//---------------------------------------------------------------------------------
// Responsible for:
// 1. Keep track of gameObjects in scene
// 2. Keep track of win/lose
// 3. Interact with other managers (i.e. UIManager, GameManager)
//---------------------------------------------------------------------------------
/*
    THINGS TO SAVE:
        > currentLevelName
        > List of fire, debris, walls, enemys
        > timeLeft
*/
public class LevelManager : MonoBehaviour, IDataPersistence
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
    public List<EnemyObject> enemyList;
    private EnemyStats[] enemies;

    [Header("Task Tracking")]
    public List<TaskItem> taskList;

    [Header("Timer Setting")]
    public float timeLeft = 60f;
    [SerializeField] private bool timerOn;
    [SerializeField] private TextMeshProUGUI timeText;
    [HideInInspector] public float timePast;

    public bool showClearCanvas = false;
    #endregion

    // =======================
    // Private
    // =======================
    private PlayerCanvas playerCanvas;
    private ScoreCanvas score;
    private PlayerInventory[] playerInventories;
    private int counter;
    private float totalTime;

    #region Unity Methods
    protected void Start()
    {
        // get current level name
        currentLevelName = SceneManager.GetActiveScene().name;

        // Comment it out if NOT starting from Boot scene
        // CheckGameStateChanged();

        playerCanvas = FindObjectOfType<PlayerCanvas>();
        score = FindObjectOfType<ScoreCanvas>();

        // reset inventory data
        playerInventories = FindObjectsOfType<PlayerInventory>();
        foreach (PlayerInventory inv in playerInventories)
        {
            inv.counter = 0;
        }

        counter = 0;

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

        // Populate Task 
        PopulateTasks();
    }

    protected void Update()
    {
        // update timer
        if (timerOn)
        {
            totalTime = timeLeft;
            timeLeft -= Time.deltaTime;
            UpdateTimer(timeLeft);

            // if timer hits 0
            if (timeLeft <= 0)
            {
                timerOn = false;
                ToggleGameOver();
            }
        }


        // if (showClearCanvas)
        // {
        //     counter = taskList.Count;
        // }

        // CheckToggleState();
    }

    #endregion
    #region Save & Load Methods
    public void LoadData(GameData data)
    {
        this.currentLevelName = data.currentLevel;
    }

    public void SaveData(GameData data)
    {
        data.currentLevel  = this.currentLevelName;
    }

    #endregion
    #region Level Methods
    /// <summary>
    /// Load the next level
    /// </summary>
    /// <param name="levelName"></param>
    public void LoadNextLevel(string levelName)
    {

        var loadSq = LeanTween.sequence();
        loadSq
        .append(1f)
        .append(() =>
        {
            FadeCamera.Instance.FadeInCanvas();

        })
        .append(2f)
        .append(() =>
        {
            // unload previous level and stop the music
            GameManager.Instance.UnloadLevel(currentLevelName);
        })
        .append(2f)
        .append(() =>
        {
            // load next level
            GameManager.Instance.LoadLevel(levelName);
        });
    }

    /// <summary>
    /// Checks for the game's state and 
    /// fires events according to state
    /// </summary>
    private void CheckGameStateChanged()
    {
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

                // update taskList
                foreach (TaskItem task in taskList)
                {
                    if (task.taskType == TaskType.Fire)
                    {
                        // Convert string to int
                        int taskCount = Convert.ToInt32(task.objectiveCount.text);

                        // increment int
                        taskCount++;

                        // Convert bacak to string
                        task.objectiveCount.text = taskCount.ToString();

                        if (taskCount == Convert.ToInt32(task.totalObjectiveCount.text))
                        {
                            task.checkBox.isOn = true;
                            counter++;
                            CheckToggleState();
                            break;
                        }
                    }

                    return;
                }
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

                // update taskList
                foreach (TaskItem task in taskList)
                {
                    if (task.taskType == TaskType.Repair)
                    {
                        // Convert string to int
                        int taskCount = Convert.ToInt32(task.objectiveCount.text);

                        // increment int
                        taskCount++;

                        // Convert bacak to string
                        task.objectiveCount.text = taskCount.ToString();

                        if (taskCount == Convert.ToInt32(task.totalObjectiveCount.text))
                        {
                            task.checkBox.isOn = true;
                            counter++;
                            CheckToggleState();
                            break;
                        }

                        return;
                    }
                }
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
                // update the debris's state
                d.isCleared = debrisState;

                // update taskList
                foreach (TaskItem task in taskList)
                {
                    if (task.taskType == TaskType.Debris)
                    {
                        // Convert string to int
                        int taskCount = Convert.ToInt32(task.objectiveCount.text);

                        // increment int
                        taskCount++;

                        // Convert bacak to string
                        task.objectiveCount.text = taskCount.ToString();

                        if (taskCount == Convert.ToInt32(task.totalObjectiveCount.text))
                        {
                            task.checkBox.isOn = true;
                            // increment counter
                            counter++;
                            CheckToggleState();
                            break;
                        }

                        return;
                    }
                }
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
            catch (System.Exception e)
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
        foreach (EnemyObject enemy in enemyList)
        {
            if (enemy.enemyId == enemyId)
            {
                // update enemy's state
                enemy.isDead = enemyState;

                // update taskList
                foreach (TaskItem task in taskList)
                {
                    if (task.taskType == TaskType.Enemy)
                    {
                        // Convert string to int
                        int taskCount = Convert.ToInt32(task.objectiveCount.text);

                        // increment int
                        taskCount++;

                        // Convert bacak to string
                        task.objectiveCount.text = taskCount.ToString();

                        if (taskCount == Convert.ToInt32(task.totalObjectiveCount.text))
                        {
                            task.checkBox.isOn = true;
                            counter++;
                            CheckToggleState();
                            break;
                        }

                        return;
                    }
                }
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

    private void PopulateTasks()
    {
        foreach (TaskItem task in taskList)
        {
            task.checkBox.isOn = false;
            task.objectiveCount.text = 0.ToString();

            // Set totalObjectiveCount
            switch (task.taskType)
            {
                case TaskType.Fire:
                    task.totalObjectiveCount.text = fireList.Count.ToString();
                    break;
                case TaskType.Repair:
                    task.totalObjectiveCount.text = wallList.Count.ToString();
                    break;
                case TaskType.Debris:
                    task.totalObjectiveCount.text = debrisList.Count.ToString();
                    break;
                case TaskType.Enemy:
                    task.totalObjectiveCount.text = enemyList.Count.ToString();
                    break;
                default:
                    Debug.LogError("[LevelManager] - TaskType not found");
                    break;
            }
        }

        timerOn = true;
    }

    private void UpdateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timeText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    private void CheckToggleState()
    {
        if (counter == taskList.Count)
        {
            // Show level clear
            Debug.LogWarning("Level Cleared");
            score.ShowClear();

            // stop timer
            timerOn = false;

            // get current time
            timePast = totalTime - timeLeft;
            Debug.Log("Time past: " + timePast);
            // reset counter
            counter = 0;
            return;
        }
    }
    #endregion
}