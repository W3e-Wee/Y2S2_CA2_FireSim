using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class TestPatrol : MonoBehaviour
{
    #region Variables
    //===================
    // Public Variables
    //===================

    //====================================
    // [SerializeField] Private Variables
    //====================================
    [SerializeField] private Transform[] wayPoints;
    private int wayPointIndex;

    [SerializeField] private float waitTime;
    private float waitCounter;

    [SerializeField, Range(0f, 1f)] private float pendingDistance = 0.5f;

    private NavMeshAgent agent;
    #endregion

    #region Unity Methods

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        wayPoints = GameObject.FindGameObjectWithTag("WayPoint").GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (agent.remainingDistance < pendingDistance && !agent.pathPending)
        {
            waitCounter += Time.deltaTime;

            if (waitCounter >= waitTime)
            {
                waitCounter = 0f;

                if (wayPoints.Length == 0)
                {
                    return;
                }

                // Go to destination
                agent.destination = wayPoints[wayPointIndex].position;

                wayPointIndex = Random.Range(0, wayPoints.Length) % (wayPoints.Length);

                // print("Current index: " + Random.Range(destIndex, wayPoints.Length));
            }
        }
    }
    #endregion

    #region Own Methods
    // private void GoToNextDest()
    // {
    //     waitCounter = 0f;

    //     if (wayPoints.Length == 0)
    //     {
    //         return;
    //     }

    //     // Go to destination
    //     agent.destination = wayPoints[wayPointIndex].position;

    //     wayPointIndex = Random.Range(0, wayPoints.Length) % (wayPoints.Length);

    //     // print("Current index: " + Random.Range(destIndex, wayPoints.Length));
    // }
    #endregion

}
