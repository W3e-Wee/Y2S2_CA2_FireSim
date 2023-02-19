//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-18
// Description	: Interface to save and load data to scripts 
//---------------------------------------------------------------------------------

public interface IDataPersistence
{
	void LoadData(GameData data);
	void SaveData(GameData data);
}
