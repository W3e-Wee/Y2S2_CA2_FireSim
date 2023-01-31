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
    public int currentHealth;

    //===================
    // Private Variables
    //===================
    [SerializeField] private bool isDead = false;

    //===================
    // Private Variables
    //===================
    private Animator anim;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        anim = this.GetComponent<Animator>();
    }

	// TEMPORARY (FOR TESTING)
    protected void Update()
    {
        if (isDead)
        {
            Die();
        }
    }
    
	#endregion

    #region Own Methods
    private void Die()
    {
        this.GetComponent<EnemyBT>().enabled = false;

        anim.SetBool("isDead", true);
    }
    public void TakeHit(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            // enemy die
            Die();
        }
    }
    #endregion
}
