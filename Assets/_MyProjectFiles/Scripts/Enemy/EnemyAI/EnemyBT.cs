using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

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
    [Header("Navigation Settings")]
    public Transform[] wayPoints;
    public NavMeshAgent agent;
    public float agentSpeed = 4;
    public float waitTime;

    // =====================
    // Range Setting
    // =====================
    [Header("Range Settings")]
    public float noticeRange;
    public float attackRange;

    // =====================
    // Keys
    // =====================
    public const string TARGET_KEY = "target";

    #region Tree Methods
    protected override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // agent.autoBraking = false;

        wayPoints = GameObject.FindGameObjectWithTag("WayPoint").GetComponentsInChildren<Transform>();

        base.Start();
    }

    protected override Node SetUpTree()
    {
        Node root = new SelectorNode(new List<Node>{
            // Attack Sequence
            new SequenceNode(new List<Node>{
                new CheckPlayerInAttackRange(agent, attackRange),
                new EnemyAttack(agent)
            }),
            // Chase Sequence
            new SequenceNode(new List<Node>{
                new CheckPlayerInRange(agent, noticeRange),
                new ChaseTarget(agent, agentSpeed)
            }),
            // Fallback Node
            new EnemyPatrol(wayPoints,agent,waitTime),
        });

        return root;
    }
    #endregion
}
