using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-02-13
// Description	: Script to follow XR Rig
//---------------------------------------------------------------------------------

[System.Serializable]
public class InventoryBag
{
	public GameObject gameObject;
	[Range(0.01f, 2f)] public float heightOffset;
	public int runeCount;
}

public class InventoryFollowPlayer : MonoBehaviour 
{
	#region Variables
	//===================
	// Public Variables
	//===================
	public Camera HMD;
	public InventoryBag[] simpleInteractables;

	//===================
	// Private Variables
	//===================
	private Vector3 currentHMDPosition;
	private Quaternion currentHMDRotation;
	#endregion
	
	#region Unity Methods	
	protected void Update() 
	{
		currentHMDPosition = HMD.transform.position;
		currentHMDRotation = HMD.transform.rotation;
		foreach(InventoryBag interactable in simpleInteractables)
		{
			UpdateInventoryBagHeight(interactable);
		}
		UpdateInteractableInventorySystem();
	}
	#endregion

	#region Own Methods
	private void UpdateInventoryBagHeight(InventoryBag invBag)
	{
		invBag.gameObject.transform.position = new Vector3(invBag.gameObject.transform.position.x, currentHMDPosition.y * invBag.heightOffset, invBag.gameObject.transform.position.z);
	}

	private void UpdateInteractableInventorySystem()
	{
		transform.position = new Vector3(currentHMDPosition.x, 0, currentHMDPosition.z);
		transform.rotation = new Quaternion(transform.rotation.x, currentHMDPosition.y, transform.rotation.z, currentHMDRotation.w);
	}
	#endregion

}
