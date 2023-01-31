using UnityEngine;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class Weapon : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================

    //====================================
    // [SerializeField] Private Variables
    //====================================
    [SerializeField] private int damage = 10;

    //===================
    // Private Variables
    //===================
	private int targetLayer = 10;
    #endregion

    #region Unity Methods

    #endregion

    #region Own Methods
    void OnCollisionEnter(Collision col)
    {
        // Debug.Log(col.gameObject.layer);
        if (col.gameObject.layer == targetLayer && col.gameObject.TryGetComponent(out EnemyStats enemy))
        {
            Debug.Log("Collided with an Enemy");
			// take dmg
			enemy.TakeHit(damage);
        }

    }
    #endregion

}
