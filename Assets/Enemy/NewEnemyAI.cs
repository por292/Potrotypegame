using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Random;
using UnityEngine.AI;
[RequireComponent(typeof(NavMeshAgent))]

public class NewEnemyAI : MonoBehaviour
{
    Transform Player;
    NavMeshAgent agent;
    //public GameObject Projectile;
    public float health = 50f;
    private enum agentAction { sprint, superJump, Reach, proj }
    private Vector3 walkPoint;
    bool startPos;
    private float startPosDis = 10f;
    public int damage = 10;


    public float InSight = 20f;
    public float InAttack = 2.5f;
    private bool AttackRange, SightRange;
    public float AttackCD = 1.5f;
    private float HasAttacked;
    private LayerMask whatIsPlayer;

    public LayerMask whatIsGround;
    // Start is called before the first frame update
    void Start()
    {

        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 2.0f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            agent.Warp(hit.position); // Safely places the agent
        }

        whatIsPlayer = LayerMask.GetMask("Player");
        whatIsGround = LayerMask.GetMask("Ground");

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
        agent.stoppingDistance = 2f;
        Player = GameObject.Find("Player").transform;

    }
    public void Patrolling()
    {
        //if (!startPos) SerchStartPos();

        //if (startPos)
        //    agent.SetDestination(walkPoint);

        //Vector3 DisToSPos = transform.position - walkPoint;

        //if (DisToSPos.magnitude < 1f)
        //    startPos = false;
        if (agent == null || !agent.isActiveAndEnabled || !agent.isOnNavMesh) return;

        if (!startPos)
            SerchStartPos();

        if (startPos)
            agent.SetDestination(walkPoint);

        Vector3 DisToSPos = transform.position - walkPoint;

        if (DisToSPos.magnitude < 1f)
            startPos = false;
    }
    private void Chase()
    {
        if (agent != null && agent.isActiveAndEnabled && agent.isOnNavMesh)
        {
            agent.SetDestination(Player.position);
        }
    }
    private void SerchStartPos()
    {
        float randomZ = UnityEngine.Random.Range(-startPosDis, startPosDis);
        float randomX = UnityEngine.Random.Range(-startPosDis, startPosDis);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))

            startPos = true;


    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        OnCollisionTrigger(collision);
    //    }
    //}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            if (player != null)
            {
                player.TakeDamage(10);
            }
        }
    }
    //private void OnCollisionTrigger(Collision collision)
    //{
    //    collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
    //}


    public void Attacking()
    {
        if (Player == null) return;

        float distance = Vector3.Distance(transform.position, Player.position);

        if (distance <= InSight)
        {
            if (distance <= InAttack)
            {
                agent.isStopped = true;
                if (Time.time >= HasAttacked )
                {
                    AttackAgain();
                    HasAttacked = Time.time + AttackCD;

                }
            }
        }
    }
    private void AttackAgain()
    {
        PlayerController PHealth = Player.GetComponent<PlayerController>();

        if (PHealth != null)
        {
            PHealth.TakeDamage(damage);
        }
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
}
