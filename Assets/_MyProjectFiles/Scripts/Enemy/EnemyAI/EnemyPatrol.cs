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
    public EnemyPatrol(Transform[] wayPoints, NavMeshAgent agent, float waitTime)
    {
        _wayPoints = wayPoints;
        _agent = agent;
        _waitTime = waitTime;
        _animator = agent.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        // when nearing the waypoint
        // _agent.remainingDistance <= _pendingDistance && !_agent.pathPending

        float distToTarget = Vector3.Distance(_agent.destination, _agent.transform.position);

        if (distToTarget < 0.01f)
        {
            _animator.SetFloat("moveBlend", 0f);

            // Waiting...
            waitCounter += Time.deltaTime;

            // Get the coord. of next waypoint
            if (waitCounter >= _waitTime)
            {
                _animator.SetFloat("moveBlend", 1f);

                GoToDest();
            }
        }


        state = NodeState.RUNNING;
        return state;
    }

    private void GoToDest()
    {
        // reset waitCounter
        waitCounter = 0f;

        // check waypoints in scene
        if (_wayPoints.Length == 0)
        {
            Debug.LogWarning("No Waypoints in scene");
            return;
        }

        // Get random waypoint index
        _wayPointIndex = Random.Range(0, _wayPoints.Length) % (_wayPoints.Length);
        Debug.Log("Way point: " + _wayPointIndex);

        // move to wayPoint
        _agent.destination = _wayPoints[_wayPointIndex].position;
    }
    #endregion
}
