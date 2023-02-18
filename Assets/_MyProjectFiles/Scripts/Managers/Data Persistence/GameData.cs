using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class GameData
{
	public string currentLevel;

	// values defined in this constructor will b defaule values
	// the game starts with when there's no data to be loaded
	public GameData()
	{
		this.currentLevel = string.Empty;
	}
}
