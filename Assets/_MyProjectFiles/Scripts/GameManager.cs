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
    public GameObject[] systemPrefabs;

    // =============================
    // [SerializedField] Private
    // =============================
    [SerializeField] private List<AsyncOperation> loadOperations;
    
    // =============================
    // Private
    // =============================
    private string currentLevelName = string.Empty;
    private List<GameObject> instancedSystemPrefabs;

    #endregion

    #region Unity Methods
    private void Start()
    {
        // prevent GameManager from being destroyed
        DontDestroyOnLoad(gameObject);

        loadOperations = new List<AsyncOperation>();
        instancedSystemPrefabs = new List<GameObject>();

        InstantiateSystemPrefabs();

        LoadLevel("Menu_Scene");
    }

    protected override void OnDestroy()
    {   
        base.OnDestroy();

        for(int i = 0; i < instancedSystemPrefabs.Count; i++)
        {
            Destroy(instancedSystemPrefabs[i]);
        }

        instancedSystemPrefabs.Clear();
    }
    #endregion

    #region Own Methods
    private void InstantiateSystemPrefabs()
    {
        GameObject prefabInstance;

        for(int i = 0; i < systemPrefabs.Length; i++)
        {
            prefabInstance = Instantiate(systemPrefabs[i]);
            instancedSystemPrefabs.Add(prefabInstance);
        }
    }

    #region Scene Management Methods
    private void onLoadOperationComplete(AsyncOperation ao)
    {
        // check to see if this methods being called elsewhere
        if (loadOperations.Contains(ao))
        {
            loadOperations.Remove(ao);
        }

        

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(currentLevelName));

        Debug.Log("Load Completed");
    }

    private void onUnloadOperationComplete(AsyncOperation ao)
    {
        Debug.Log("Unload Completed");
    }

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
        ao.completed += onLoadOperationComplete;

        currentLevelName = levelName;
    }

    public void UnloadLevel(string levelName)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(levelName);
        ao.completed += onUnloadOperationComplete;
    }
    #endregion

    #endregion
}
