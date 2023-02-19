using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-02
// Description	: Script for Game Manager functionalities.
//---------------------------------------------------------------------------------
// Game Manager Functionalities
// 1. Load and Unload levels
// 2. Keep track of what level the game is in
// 3. Keep track of game state
// 4. Generate other persistent systems
//---------------------------------------------------------------------------------

public class GameManager : Singleton<GameManager>
{
    #region Variables
    // =============================
    // Public
    // =============================
    public enum GameState
    {
        PREGAME,
        RUNNING,
        PAUSED
    }
    public GameObject[] systemPrefabs;

    [Header("Events")]
    public Events.EventGameState OnGameStateChanged;

    // =============================
    // [SerializedField] Private
    // =============================
    [SerializeField] private List<AsyncOperation> loadOperations;

    // =============================
    // Private
    // =============================
    private string currentLevelName = string.Empty;
    private List<GameObject> instancedSystemPrefabs;
    private GameState currentGameState = GameState.PREGAME;
    #endregion

    #region Unity Methods
    private void Start()
    {
        // prevent GameManager from being destroyed
        DontDestroyOnLoad(gameObject);

        loadOperations = new List<AsyncOperation>();
        instancedSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();

        for (int i = 0; i < instancedSystemPrefabs.Count; i++)
        {
            Destroy(instancedSystemPrefabs[i]);
        }

        instancedSystemPrefabs.Clear();
    }
    #endregion

    #region Game State Methods

    /// <summary>
    /// Used to change GameState in other scripts
    /// </summary>
    /// <value></value>
    public GameState CurrentGameState
    {
        get { return currentGameState; }
        set { currentGameState = value; }
    }
    
    /// <summary>
    /// Called to add prefabs to list
    /// </summary>
    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;

        for (int i = 0; i < systemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    /// <summary>
    /// Triggers actions/events related to GameState
    /// </summary>
    /// <param name="state"></param>
    private void UpdateState(GameState state)
    {
        GameState previousGameState = currentGameState;
        currentGameState = state;

        switch (currentGameState)
        {
            case GameState.PREGAME:
                break;
            case GameState.RUNNING:
                break;
            case GameState.PAUSED:
                break;
            default:
                break;
        }

        // dispatch message
        OnGameStateChanged.Invoke(currentGameState, previousGameState);
    }
    #endregion

    #region Scene Loading Methods

    /// <summary>
    /// Trigger actions/events when new scene is loaded
    /// </summary>
    /// <param name="ao"></param>
    private void OnLoadOperationComplete(AsyncOperation ao)
    {
        // check to see if this methods being called elsewhere
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }

        // check if there are any loadOperations running
        if (loadOperations.Count == 0)
        {
            UpdateState(GameState.RUNNING);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLevelName));
        }

        Debug.Log("Load Completed");
    }

    /// <summary>
    /// Triggers actions/events when a scene is unloaded
    /// </summary>
    /// <param name="ao"></param>
    private void OnUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Completed");
    }

    /// <summary>
    ///  Loads a new scene with matching levelName
    /// </summary>
    /// <param name="levelName"></param>
    public void LoadLevel(string levelName)
    {
        // load scene and save AsyncOperation (ao)
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Additive);

        // check for any problems
        if (ao == null)
        {
            Debug.LogError("[GameManager] An error occured when loading scene, " + currentLevelName);
            return;
        }

        // when no problem
        ao.completed += OnLoadOperationComplete;

        currentLevelName = levelName;
    }

    /// <summary>
    /// Unloads a scene with matching levelName
    /// </summary>
    /// <param name="levelName"></param>
    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.UnloadSceneAsync(levelName);

        ao.completed += OnUnloadOperationComplete;
    }
    #endregion

    #region Game Methods
    /// <summary>
    /// Start the game
    /// </summary>
    public void StartGame()
    {
        LoadLevel("Menu_Scene");
    }

    /// <summary>
    /// Method to exit the game
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Debug.Log("Exiting Application...");
        Application.Quit();
    }
    #endregion
}
