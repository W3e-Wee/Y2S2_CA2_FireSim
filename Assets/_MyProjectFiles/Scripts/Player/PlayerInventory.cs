using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-13
// Description	: Script to spawn gameobject onto Player's hands
//---------------------------------------------------------------------------------

public class PlayerInventory : MonoBehaviour 
{
	#region Variables
	//===================
	// Public Variables
	//===================
	public GameObject itemPrefab;

	#endregion

	#region Own Methods
	public void SpawnGameObjectOnHand()
	{
		Instantiate(itemPrefab, transform.position, Quaternion.identity);
	}
	#endregion

}
