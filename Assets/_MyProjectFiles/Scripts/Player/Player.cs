using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-16
// Description	: Manages the player's stats
//---------------------------------------------------------------------------------

public class Player : MonoBehaviour 
{
	#region Variables
	public bool isPlayerDead;
	//====================================
	// [SerializeField] Private Variables
	//====================================
	[SerializeField] private int maxHealth;
	[SerializeField] private int currentHealth;

	//===================
	// Private Variables
	//===================
	#endregion
	
	#region Unity Methods
	protected void Start()
	{
		currentHealth = maxHealth;
		isPlayerDead = false;
	}
	#endregion

	#region Own Methods
	public void TakeDmg(int damage)
	{
		// Play an effect to show player hit

		print("Player hit");
		currentHealth -= damage;

		if(currentHealth <= 0)
		{
			isPlayerDead = true;
		}
		
		isPlayerDead = false;
	}
	#endregion

}
