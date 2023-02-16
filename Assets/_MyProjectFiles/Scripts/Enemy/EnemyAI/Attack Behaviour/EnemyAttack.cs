using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class EnemyAttack : Node
{
    private NavMeshAgent _agent;
    private Animator _anim;
    private float attackCounter;
    private float attackCD = 1f;

    #region Node Methods
    public EnemyAttack(NavMeshAgent agent)
    {
        _agent = agent;
        _anim = agent.transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        // Check if the target exists
        Transform target = (Transform)GetData(EnemyBT.TARGET_KEY);

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }

        // Get player script
        Player _player = target.GetComponent<Player>();

        // Play attacking animation
        _anim.SetBool("isAttacking", true);


        Debug.Log("Attacking");

        attackCounter += Time.deltaTime;
        
        if (attackCounter >= attackCD)
        {
            bool playerDead = _player.TakeDmg();

            // Check if player is dead
            if (playerDead)
            {
                ClearData(EnemyBT.TARGET_KEY);
                _agent.stoppingDistance = 0;
                _agent.speed = 2f;

                Debug.Log("Player is dead." + target);

                // stop attack anim
                _anim.SetBool("isAttacking", false);
                _anim.SetFloat("moveBlend", 0f);
            }
            else
            {
                attackCounter = 0f;
            }
        }


        state = NodeState.RUNNING;
        return state;
    }

    #endregion
}
