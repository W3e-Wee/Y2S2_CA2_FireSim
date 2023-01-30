using UnityEngine;
using UnityEngine.AI;
using System.Collections;

//---------------------------------------------------------------------------------
// Author		: Wee Heng
// Date  		: YYYY-MM-DD
// Description	: This is where you write a summary of what the role of this file.
//---------------------------------------------------------------------------------

public class EnemyBT : Tree
{
    // =====================
    // Navigation Setting
    // ====================
    public Transform[] wayPoints;
    public NavMeshAgent agent;
    public float waitTime;
    public float pendingDistance;

    protected override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;

        wayPoints = GameObject.FindGameObjectWithTag("WayPoint").GetComponentsInChildren<Transform>();
        
		base.Start();
    }
    protected override Node SetUpTree()
    {
        Node root = new EnemyPatrol(wayPoints, agent, waitTime, pendingDistance);

        return root;
    }
}
