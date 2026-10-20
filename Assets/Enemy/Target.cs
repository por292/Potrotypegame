using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
//using UnityEngine.Debug;
//using System.Diagnostics.Debug;

public class Target : MonoBehaviour
{

    public float MaxHealth = 20f;
    private float CurrentHealth;
    //[SerializeField] public AudioClip damageSoundC;
    //
    //  [SerializeField] public AudioClip deathSoundC;

    public bool HasTakenDamage { get; set; }


    public Transform player;
    public NavMeshAgent agent;
    public GameObject Projectile;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health = 50f;
    public float distanceBetweenAB = 20f;

    // partol
    public Vector3 walkPoint;
    public float walkPointSet;
    public bool isBetweenRange;
    public float Chase;

    //attacks
    public float timedAttacks;
    bool alreadyAttacking;

    //states 
    public float sightRange, attackRange;
    public bool playerInSight, playerInAttackingRange;

    private void Awake()
    {
        //player = GameObject.Find("playerObj").transform;
        //agent = GetComponent<NavMeshAgent>();
    }
   /* private void Patroling()
    {
        if (!isBetweenRange) SearchWalkPoint();

        if (isBetweenRange)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;  
        
        if (distanceToWalkPoint.magnitude < 1f)
            isBetweenRange = false;
    }
    private void SearchWalkPoint()
    {
       
        float randomZ = Random.Range(-walkPointSet, walkPointSet);
        float randomX = Random.Range(-walkPointSet, walkPointSet);

        walkPoint = new Vector3(transform.position.x + randomX + transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround));
        
    }
    private void chase()
    {
        agent.SetDestination(player.position);

    }
    private void Attacking()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacking)
        {

            Rigidbody rb = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();

            alreadyAttacking = true;
            Invoke(nameof(ResetAttack), timedAttacks);
        }

    }
    private void ResetAttack()
    {
        alreadyAttacking = false;   
    }

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (walkPointSet < distanceBetweenAB)
        {
            isBetweenRange = true;
        }
        if (isBetweenRange)
        {
            // follow the player
        }
        playerInAttackingRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        playerInSight = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInSight && !playerInAttackingRange) Patroling();
        if (playerInSight && !playerInAttackingRange) chase();
        if (playerInSight && playerInAttackingRange) Attacking();
    }*/
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
    public void takeDamage(float amount)
    {
        HasTakenDamage = true;
        CurrentHealth -= amount;


        CurrentHealth -= amount;
        //Debug(CurrentHealth);
        if (CurrentHealth <= 0f)
        {
            Die();
            
        }
    }
    private void Update()
    {
        if (transform.position.y <= -20)
        {
            Destroy(gameObject);
        }
    }
}
