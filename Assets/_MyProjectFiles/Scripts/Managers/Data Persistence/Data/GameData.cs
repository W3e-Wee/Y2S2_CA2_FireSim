using UnityEngine;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-19
// Description	: Stores the current game data
//---------------------------------------------------------------------------------

[System.Serializable]
public class GameData
{
	public string currentLevel;
	
	// List of objectives to save;
	// public List<FireObject> sceneFires;
	// public List<ConstructObject> sceneWalls;
	// public List<DebrisObject> sceneDebris;
	// public List<EnemyObject> sceneEnemy;

	// public bool newGame;

	// values defined in this constructor will b defaule values
	// the game starts with when there's no data to be loaded
	#region Constuctor
	public GameData()
	{
		this.currentLevel = "MainLevel_01_Scene";

		// List of objectives to save
		// this.sceneFires = new List<FireObject>();
		// this.sceneWalls = new List<ConstructObject>();
		// this.sceneDebris = new List<DebrisObject>();
		// this.sceneEnemy = new List<EnemyObject>();
	}

	#endregion
}
