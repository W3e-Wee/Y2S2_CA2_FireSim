using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: Script to check for target in range
//---------------------------------------------------------------------------------

public class CheckPlayerInRange : Node
{
    private int _layerMask = 1 << 7;
    private NavMeshAgent _agent;
    private float _noticeRange;

    public CheckPlayerInRange(NavMeshAgent agent, float noticeRange)
    {
        _agent = agent;
        _noticeRange = noticeRange;
    }
    public override NodeState Evaluate()
    {
        object t = GetData(EnemyBT.TARGET_KEY);

		if (t == null)
        {
            // gets target in range
            Collider[] colliders = Physics.OverlapSphere(
                _agent.transform.position,
                _noticeRange,
                _layerMask
            );

            // save the data
            if (colliders.Length > 0)
            {
                parent.parent.SetData(EnemyBT.TARGET_KEY, colliders[0].transform);

                state = NodeState.SUCCESS;
                return state;
            }

			// no target in range
            state = NodeState.FAILURE;
            return state;
        }

		// target already present
        state = NodeState.SUCCESS;
        return state;
    }

    private void CheckInRange()
    {

    }
}
