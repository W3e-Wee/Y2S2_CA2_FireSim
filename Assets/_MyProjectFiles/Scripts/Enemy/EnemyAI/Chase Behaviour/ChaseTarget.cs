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
	private float _speed;
	public ChaseTarget(NavMeshAgent agent, float agentSpeed)
	{
		_agent = agent;
		_speed = agentSpeed;
	}

    public override NodeState Evaluate()
    {
		// // get target transform
        Transform target = (Transform)GetData(EnemyBT.TARGET_KEY);
		
		// // move to target if spotted
		if(Vector3.Distance(_agent.transform.position, target.position) >= 0.01f)
		{
			// increase agent speed
			_agent.speed = _speed;
		
			// go to target
			_agent.destination = target.position;
		}
		
		state = NodeState.RUNNING;
		return state;
    }
}
