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
    //====================================
    // [SerializeField] Private Variables
    //====================================
    [Header("Player Stats")]
    [SerializeField] private int currentHealth;
    [SerializeField] private int dmgTaken = 10;
    
    [Space]
    public bool isPlayerDead;
    //===================
    // Private Variables
    //===================
    private LevelManager levelManager;
    #endregion

    #region Unity Methods
    protected void Start()
    {
        currentHealth = 100;
        isPlayerDead = false;

        levelManager = FindObjectOfType<LevelManager>();
        if(levelManager == null)
        {
            Debug.LogError("[Player] - Level Manager not found in scene");
            return;
        }
    }
    #endregion

    #region Own Methods
    public bool TakeDmg()
    {
        currentHealth -= dmgTaken;

        if (currentHealth <= 0)
        {
            isPlayerDead = true;
            Die();
            return isPlayerDead;
        }

        return isPlayerDead;
    }

    private void Die()
    {
        this.GetComponent<CapsuleCollider>().enabled = false;
        // play Gameover canvas
        // levelManager.ToggleGameOver();
    }
    #endregion

}
