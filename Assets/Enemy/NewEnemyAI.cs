using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Random;
using UnityEngine.AI;

public class NewEnemyAI : MonoBehaviour
{
    Transform Player;
    NavMeshAgent agent;
    public enum agentAction { sprint, superJump, wank, proj}
    public float InSight, InAttack;
    public bool AttackRange, SightRange;
    public LayerMask whatIsPlayer, whatIsGround;
    // Start is called before the first frame update
    void Start()
    {
        agentAction values = 0;

        int randAgentAction = UnityEngine.Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        SightRange = Physics.CheckSphere(transform.position, InSight, whatIsPlayer);
        AttackRange = Physics.CheckSphere(transform.position, InAttack, whatIsPlayer);
        
        if (!SightRange && !AttackRange) Partoling();
        if (SightRange && !AttackRange) agent.SetDestination(Player.position);
        if (SightRange && AttackRange) Attacking();
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 3f;
        agent.stoppingDistance = 2.5f;
        Player = GameObject.Find("Player").transform;
        Debug.Log("funny");

    }
    public void Partoling()
    {

    }

    public void Attacking()
    {

    }


    public void randAction(agentAction action, string targetObject)
    {
        switch (action)
        {
            case agentAction.sprint://random action here
                break;
                // Handle other custom commands
        }
    }
}
