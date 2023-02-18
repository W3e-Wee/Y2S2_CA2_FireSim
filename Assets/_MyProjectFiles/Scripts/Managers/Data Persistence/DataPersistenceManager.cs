using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-18
// Description	: Persist data throughout game's lifetime
//---------------------------------------------------------------------------------

public class DataPersistenceManager : Singleton<DataPersistenceManager>
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    public GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler dataHandler;

    #region Unity Methods
    void Start()
    {
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();

        // FOR TESTING
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        NewGame();
    }
    #endregion
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        // TODO - Load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        //if no data can be loaded, init a new game
        if (this.gameData == null)
        {
            Debug.LogWarning("No data was found. Initializing data to defaults");
            NewGame();
        }

        // TODO - push the loaded data to all other scripts that need it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        // TODO - pass the data to other scripts so they can update it
        foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
        {
            dataPersistenceObj.SaveData(gameData);
        }

        // TODO - save that data to a file using the data handler
        dataHandler.Save(gameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>()
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
