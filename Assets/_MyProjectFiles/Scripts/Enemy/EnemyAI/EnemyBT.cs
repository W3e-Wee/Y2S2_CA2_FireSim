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
    public float waitTime;

    [Header("Range Settings")]
    public float noticeRange;
    public float attackRange;

    public const string TARGET_KEY = "target";

    #region Tree Methods
    protected override void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        // agent.stoppingDistance = attackRange;

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
                new ChaseTarget(agent)
            }),
            // Fallback Node
            new EnemyPatrol(wayPoints,agent,waitTime),
        });

        return root;
    }
    #endregion
}