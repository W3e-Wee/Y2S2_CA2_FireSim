using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class Debris : MonoBehaviour 
{
	#region Variables
	//===================
	// Public Variables
	//===================
	
	//====================================
	// [SerializeField] Private Variables
	//====================================
	[SerializeField] private float totalClearGauge = 0f;
	[SerializeField] private Vector3 debrisReduction; 

	//===================
	// Private Variables
	//===================

	#endregion
	
	#region Unity Methods
	
	#endregion

	#region Own Methods
	public void ClearingDebris(float clearAmount)
	{
		print("Clearing Debris...");

		// decrease gauge
		totalClearGauge -= clearAmount;

		// update progress bar

		// reduce scale of debris
		// ReduceScale(debrisReduction);

		if(totalClearGauge <= 0)
		{
			// show an effect

			// update task
		}
	}

	private void ReduceScale(Vector3 reduceNum)
	{
		Vector3 debrisScale = this.gameObject.transform.localScale;
		debrisScale -= reduceNum;
	}
	#endregion

}
