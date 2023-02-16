using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

[System.Serializable]
public class FireObject
{
    public string fireId;
    public Vector3 firePosition;
    public GameObject firePrefab;
    public bool isExtinguished;

}
