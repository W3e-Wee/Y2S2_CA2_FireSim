using UnityEngine;
using UnityEngine.UI;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: 2023-01-13
// Description	: Script managing enemy stats
//---------------------------------------------------------------------------------

public class EnemyStats : MonoBehaviour
{
    #region Variables
    //====================================
    // Public Variables
    //====================================
    public string enemyId = string.Empty;
    public int currentHealth;
    public bool isDead = false;
    public Image healthBar;

    //===================
    // Private Variables
    //===================
    private Animator anim;
    private LevelManager levelManager;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        // get Animator
        anim = this.GetComponent<Animator>();

        // fill up bar
        healthBar.fillAmount = currentHealth;
        levelManager = FindObjectOfType<LevelManager>();
    }

	// TEMPORARY (FOR TESTING)
    // protected void Update()
    // {
    //     if (isDead)
    //     {
    //         Die();
    //     }
    // }
    
	#endregion

    #region Own Methods
    private void Die()
    {
        this.GetComponent<EnemyBT>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;

        anim.SetBool("isDead", true);
    }
    public void TakeHit(int damage)
    {   
        // decrease health
        currentHealth -= damage;

        // update the healthbar
        healthBar.fillAmount = currentHealth;

        if (currentHealth <= 0)
        {
            isDead = true;
            
            // enemy die
            Die();
            levelManager.UpdateEnemyState(enemyId, isDead);
        }
    }

    [ContextMenu("Generate GUID")]
    private void GenerateGUID()
    {
        enemyId = System.Guid.NewGuid().ToString();
    }
    #endregion
}
