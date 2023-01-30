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
	public EnemyAttack(NavMeshAgent agent)
	{
		_agent = agent;
		_anim = agent.transform.GetComponent<Animator>();
	}
    public override NodeState Evaluate()
    {
		
		Transform target = (Transform)GetData(EnemyBT.TARGET_KEY);
		Player _player = target.GetComponent<Player>();

		if(_player == null)
		{
			state = NodeState.FAILURE;
			return state;
		}
		// play attacking animation
		Debug.Log("Attacking");

		// check if player is dead
		bool playerDead = _player.isPlayerDead;
		if(playerDead)
		{
			ClearData(EnemyBT.TARGET_KEY);
			// stop attack anim
			// enable walk
		}

        state = NodeState.RUNNING;
		return state;
    }
}
