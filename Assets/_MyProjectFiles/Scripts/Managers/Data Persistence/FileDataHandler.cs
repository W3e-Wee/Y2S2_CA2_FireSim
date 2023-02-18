using UnityEngine;
using System.Collections;
using System.IO;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class FileDataHandler : MonoBehaviour
{
    #region Variables
    private string dataDirPath = string.Empty;
    private string dataFileName = string.Empty;

    #endregion

    #region Unity Methods

    #endregion

    #region Own Methods
    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
		GameData loadedData = null;
		if(File.Exists(fullPath))
		{
			try
			{
				// load serialized data from the file
				string dataToLoad = string.Empty;

				using(FileStream stream = new FileStream(fullPath, FileMode.Open))
				{
					using(StreamReader reader = new StreamReader(stream))
					{
						dataToLoad = reader.ReadToEnd();
					}

					// deserialize the data from JSPN back into C# object
					loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
				}
				
			}
			catch(System.Exception e)
			{

			}
		}

		return loadedData;
    }

    public void Save(GameData data)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            // create the directory the file will be written to if it doesn't exist
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // serialize the C# game data object into JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            // write serialized data to the file
            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }
    }
    #endregion

}
