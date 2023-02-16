using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-16
// Description	: Template for storing specific wall data
//---------------------------------------------------------------------------------

[System.Serializable]
public class ConstructObject
{
    public string wallId;
    public Vector3 wallPosition;
    public GameObject wallPrefab;
    public bool isFixed;

}
