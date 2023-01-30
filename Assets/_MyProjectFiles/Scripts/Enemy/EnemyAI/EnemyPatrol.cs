using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class EnemyPatrol : Node
{
    // ====================
    // Waypoint Setting
    // ====================
    private Transform[] _wayPoints;
    private int _wayPointIndex;
    // ====================
    // Navigation Setting
    // ====================
    private NavMeshAgent _agent;
    private float _pendingDistance;

    // ====================
    // Time Setting
    // ====================
    private float _waitTime;
    private float waitCounter = 0f;
    private bool waiting = false;

    // =====================
    // Animation
    // =====================
    private Animator _animator;

    #region Own Methods
    public EnemyPatrol(Transform[] wayPoints, NavMeshAgent agent, float waitTime, float distance)
    {
        _wayPoints = wayPoints;
        _agent = agent;
        _waitTime = waitTime;
        _pendingDistance = distance;
        _animator = agent.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        // when nearing the waypoint
        if (_agent.remainingDistance <= _pendingDistance && !_agent.pathPending)
        {
            waitCounter = _waitTime;

            if (_wayPoints.Length == 0)
            {
                state = NodeState.FAILURE;
                return state;
            }

            // Go to destination
            _agent.destination = _wayPoints[_wayPointIndex].position;
            Debug.Log("Current target: " + _wayPoints[_wayPointIndex].position);

            if (_agent.remainingDistance <= 0f && waitCounter >= 0f)
            {
                Debug.Log("Current wait: " + waitCounter);
                _animator.SetBool("isPatrolling", false);
                
                waitCounter -= Time.deltaTime;
            }

            _wayPointIndex = Random.Range(0, _wayPoints.Length) % (_wayPoints.Length);
        }


        state = NodeState.RUNNING;
        return state;
    }
    #endregion
}
