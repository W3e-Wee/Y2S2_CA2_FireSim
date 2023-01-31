using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class CheckPlayerInAttackRange : Node
{
	#region Variable
	private NavMeshAgent _agent;
	private float _attackRange;
	private Animator _anim;
	#endregion

	public CheckPlayerInAttackRange(NavMeshAgent agent, float attackRange)
	{
		_agent = agent;
		_attackRange = attackRange;
		_anim = _agent.transform.GetComponent<Animator>();
	}
	#region Node Methods
    public override NodeState Evaluate()
    {
		object t = GetData(EnemyBT.TARGET_KEY);

		// check if a target is present
		if(t == null)
		{
			state = NodeState.FAILURE;
			return state;
		}

		// gets Transform of the target
		Transform target = (Transform)t;
		_agent.stoppingDistance = _attackRange;

		if(Vector3.Distance(_agent.transform.position, target.position) <= _agent.stoppingDistance)
		{
			// stop chasing, get ready to attack
			_anim.SetFloat("moveBlend", 0f);

			state = NodeState.SUCCESS;
			return state;
		}
        
		state = NodeState.FAILURE;
		return state;
    }

	#endregion
}
