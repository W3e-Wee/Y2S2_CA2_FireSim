using UnityEngine;
using System.Collections.Generic;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class GameData
{
	public string currentLevel;
	
	// List of objectives to save;
	public List<FireObject> sceneFires;
	public List<ConstructObject> sceneWalls;
	public List<DebrisObject> sceneDebris;
	public List<EnemyObject> sceneEnemy;
	// values defined in this constructor will b defaule values
	// the game starts with when there's no data to be loaded
	public GameData()
	{
		this.currentLevel = "MainLevel_01_Scene";

		// List of objectives to save
		this.sceneFires = new List<FireObject>();
		this.sceneWalls = new List<ConstructObject>();
		this.sceneDebris = new List<DebrisObject>();
		this.sceneEnemy = new List<EnemyObject>();
	}
}
