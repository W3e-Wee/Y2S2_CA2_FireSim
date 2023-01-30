using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class ChaseTarget : Node
{
	private NavMeshAgent _agent;

	public ChaseTarget(NavMeshAgent agent)
	{
		_agent = agent;
	}

    public override NodeState Evaluate()
    {
		// // get target transform
        Transform target = (Transform)GetData(EnemyBT.TARGET_KEY);
		
		// // move to target if greater than stopping distance
		if(Vector3.Distance(_agent.transform.position, target.position) >= 0)
		{
			_agent.destination = target.position;
		}
		
		state = NodeState.RUNNING;
		return state;
    }
}
