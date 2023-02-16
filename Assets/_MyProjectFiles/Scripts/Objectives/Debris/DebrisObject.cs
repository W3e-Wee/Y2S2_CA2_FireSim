using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-16
// Description	: Template for storing specific debris data
//---------------------------------------------------------------------------------

[System.Serializable]
public class DebrisObject
{
    public string debrisId;
    public Vector3 debrisPosition;
    public GameObject debrisPrefab;
    public bool isCleared;
}
