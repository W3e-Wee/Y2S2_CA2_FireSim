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
    private Animator _anim;
    private float _noticeRange;

    public CheckPlayerInRange(NavMeshAgent agent, float noticeRange)
    {
        _agent = agent;
        _noticeRange = noticeRange;
        _anim = agent.transform.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        // retrieve target (if any)
        object t = GetData(EnemyBT.TARGET_KEY);

        if (t == null)
        {
            // gets target in range
            Collider[] colliders = Physics.OverlapSphere(
                _agent.transform.position,
                _noticeRange,
                _layerMask
            );

            // save the data, if found
            if (colliders.Length > 0)
            {
                parent.parent.SetData(EnemyBT.TARGET_KEY, colliders[0].transform);

                // set animation to running
                _anim.SetFloat("moveBlend", 2f);

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
