using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-13
// Description	: Script managing enemy stats
//---------------------------------------------------------------------------------

public class EnemyStats : MonoBehaviour 
{
	#region Variables
	//====================================
	// [SerializeField] Private Variables
	//====================================
	[SerializeField]private int maxHealth;
	[SerializeField]private int currentHealth;

	[SerializeField] private int attackDmg;

	//===================
	// Private Variables
	//===================

	#endregion
	
	#region Unity Methods
	protected void Start()
	{
		currentHealth = maxHealth;
	}
	#endregion

	#region Own Methods
	public void TakeDmg(int damage)
	{
		currentHealth -= damage;
		if(currentHealth <= 0)
		{
			// enemy die
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.TryGetComponent(out Player player))
		{
			player.TakeDmg(attackDmg);
		}
		if(collision.transform.TryGetComponent(out Weapon axe))
		{
			currentHealth -= axe.attackDmg;
		}
	}
	#endregion

}
