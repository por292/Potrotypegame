using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Random;
using UnityEngine.AI;

public class NewEnemyAI : MonoBehaviour
{
    Transform Player;
    NavMeshAgent agent;
    public GameObject Projectile;
    public float health;
    public enum agentAction { sprint, superJump, Reach, proj }
    public Vector3 walkPoint;
    bool startPos;
    public float startPosDis;


    public float InSight = 20f;
    public float InAttack = 15f;
    public bool AttackRange, SightRange;
    public float AttackCD;
    bool HasAttacked;

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

        if (!SightRange && !AttackRange) Patrolling();
        if (SightRange && !AttackRange) Chase();
        if (SightRange && AttackRange) Attacking();
    }
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 3f;
        agent.stoppingDistance = 2.5f;
        Player = GameObject.Find("Player").transform;

    }
    public void Patrolling()
    {
        if (!startPos) SerchStartPos();

        if (startPos)
            agent.SetDestination(walkPoint);

        Vector3 DisToSPos = transform.position - walkPoint;

        if (DisToSPos.magnitude < 1f)
            startPos = false;
    }
    private void Chase()
    {
        agent.SetDestination(Player.position);
    }
    private void SerchStartPos()
    {
        float randomZ = UnityEngine.Random.Range(-startPosDis, startPosDis);
        float randomX = UnityEngine.Random.Range(-startPosDis, startPosDis);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))

            startPos = true;


    }


    public void Attacking()
    {
        agent.SetDestination(Player.position);

        transform.LookAt(Player);
        if (!HasAttacked)
        {

            Rigidbody rb = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            HasAttacked = true;
            Invoke(nameof(AttackAgain), AttackCD);
        }



    }
    private void AttackAgain()
    {
        HasAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, InAttack);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, InSight);
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
