using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: Template
//---------------------------------------------------------------------------------

[System.Serializable]
public class EnemyObject
{
	public string enemyId;
	public Vector3 spawnPosition;
	public GameObject enemyPrefab;
	public bool isDead;
}
