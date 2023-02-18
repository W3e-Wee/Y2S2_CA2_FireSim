using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: Interface to save and load data to scripts 
//---------------------------------------------------------------------------------

public interface IDataPersistence
{
	void LoadData(GameData data);
	void SaveData(GameData data);
}
