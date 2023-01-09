using UnityEngine;
using UnityEngine.UI;
using System;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-06
// Description	: Script handling wall repairing mechanic and animation
//---------------------------------------------------------------------------------
/*

	REPLACE "Visual feedback here" WITH ACTUAL VISUAL EFFECTS (animations, color change etc.)

*/
public class Construct : MonoBehaviour 
{
	#region Variables
	//====================================
	// [SerializeField] Private Variables
	//====================================
	[SerializeField] private Image barFill;
	[SerializeField] private float totalNeededRepair = 0f;
	[SerializeField] private float repairAmount = 0f;
	//===================
	// Private Variables
	//===================
	// private TaskManager task;
	#endregion
	
	#region Unity Methods
	protected void Start()
	{
		// Get TaskManager gameObject
		// task = GameObject.FindGameObjectWithTag("TaskManager").GetComponent<TaskManager>();

		// Set bar fill amount
		// barFill.fillAmount = repairAmount;
	}
	#endregion

	#region Own Methods
	public bool WallRepairing(float amount)
	{
		bool isRepaired = false;

		// Increase repairAmount
		repairAmount += amount;

		// Set progress bar 
		double fillAmount = Math.Round((repairAmount / totalNeededRepair), 2);
		// barFill.fillAmount = (float)fillAmount;

		// Switch case to show repair progress
		switch((float)fillAmount)
		{
			case(.25f):
				print("Repair at 25%");

				// Visual feedback here

				break;
			case(.50f):
				print("Repair at 50%");

				// Visual feedback here

				break;
			case(.75f):
				print("Repair at 75%");

				// Visual feedback here

				break;
			case(1.00f):
				print("Wall Repaired");

				// Visual feedback here
				
				isRepaired = true; 
				break;
		} // End of switch case

		return isRepaired;
	}
	#endregion

}
