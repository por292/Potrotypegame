using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using Unityengin.AI;
//using UnityEngine.Debug;
//using System.Diagnostics.Debug;

public class Target : MonoBehaviour
{

    public Transform player;
    public NavMeshAgent agent;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health = 50f;

    // partol
    public Vector3 walkPoint;
    public bool walkPointSet;
    public float Chase;

    //attacks
    public timedAttacks;
    public bool alreadyAttacking;

    //states 
    public float sightRange, attackingRange;
    public bool playerInSight, playerInAttackingRange;

    private void Awake()
    {
        player = GameObject.Find("playerObj").transform;
        agent = GetComponent<NavMechAgent>();
    }
    private void Partoling()
    {

    }
    private void Chase()
    {

    }
    private void Attacking()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerInAttackingRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }
    public void TakeDamage(float amount)
    {
        health -= amount;
        //Debug.Log(health);
        if (health <= 0f)
        {
            Die();
        }
    }
   
    void Die()
    {
        Destroy(gameObject);
    }
}
