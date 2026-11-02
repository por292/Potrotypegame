using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private CharacterInput controls; // Our controls we defined
    private Vector3 velocity; // To hold our current velocity
    private Vector2 move; // To hold our move

    [Header("Health Settings")]
    public float PHealth = 100f;
    private float currentHealth;
    private bool isDead = false;
    public Animator animator;

    private CharacterController controller; // Character controller

    [Header("Movement")]
    private float moveSpeed = 6f; // To calibrate our movement
    private float jumpHeight = 2.4f; // To calibrate our jump
    private float gravity = -9.81f; // To calibrate our gravity
    private float sprintSpeed = 10f;

    [Header("Dashing")]
    private float dashing = 10f;
    [SerializeField] private float CDT = 2f;
    private bool canTrigger = true;
    private Transform ground; // Ground check empty goes in here
    private float distanceToGround = 0.4f; // How close the ground needs to be to register
    private LayerMask groundMask; // What layer is ground?


    private float dashSpeed = 20f;
    private float dashDuration = 0.2f;
    private bool isDashing = false;
    private Vector3 dashDirection;
    private Vector3 dashStartPosition;
    private float dashTimeElapsed;
    private float maxDashDistance;

    [SerializeField] private PlayerGunSelect GunSelect;

    private float damage = 5f;
    private float range = 100f;
    public Camera fpsCam;

    void Awake()
    {
		controls = new CharacterInput();
		controller = GetComponent<CharacterController>();
    }
    private void Start()
    {
        currentHealth = PHealth;
    }

    void Update()
    {
        if (controls.Player.Shoot.triggered)
        {
            Shoot();
        }
        
		PlayerMovement();
		Grav();
		Jump();
        Dashing();
        TriggerAction();
        if (controls.Player.Dash.IsPressed())
        {
            StartDash();
        }

        if (isDashing)
        {
            Dashing();
        }
        
    }
    public void TakeDamage(int damage)
    {
        PHealth -= damage;
        if (PHealth <= 0) Die();
    }
    private void Die()
    {
        isDead = true;

        if (TryGetComponent<PlayerController>(out PlayerController movement))
        {
            movement.enabled = false;
        }
        if (animator != null)
        {
            animator.SetTrigger("isDead");
        }
        Invoke("RestartLevel", 2f);
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Mouse.current.leftButton.isPressed && GunSelect.ActiveGun != null)
        {
            GunSelect.ActiveGun.shoot();
            Debug.Log("shooting");
            Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.green, 1f);
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);
                Target enemy = hit.transform.GetComponent<Target>();

                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }

            }
            else
            {
                Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * hit.distance, Color.green, 1f);
            }
             Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * range, Color.green, 1f);
        }
    }


private void Grav()
    {
		if (isGrounded() && velocity.y < 0){
			velocity.y = -2f;
		}
		
		velocity.y += gravity * Time.deltaTime;
		controller.Move(velocity * Time.deltaTime);
    }

    private bool isGrounded()
    {
		return Physics.CheckSphere(ground.position, distanceToGround, groundMask);
        //Dashing();
    }
    private void StartDash()
    {
        TriggerAction();
        isDashing = true;
        dashStartPosition = transform.position;
        dashTimeElapsed = 0f;

        float horizontal = velocity.x; //Input.GetAxisRaw("Horizontal");
        float vertical = velocity.y; //Input.GetAxisRaw("Vertical");
        dashDirection = new Vector3(horizontal, 0f, vertical).normalized;
        
        if (dashDirection == Vector3.zero)
        {
            dashDirection = transform.forward;
        }
    }
    private void StopDash()
    {
        isDashing=false;
    }

    private void Dashing()
    {
        if (controls.Player.Dash.IsPressed())
        { 
         TriggerAction();
         StartDash();
         transform.position += dashDirection * dashSpeed * Time.deltaTime;
         dashTimeElapsed += Time.deltaTime;
         float distanceTraveled = Vector3.Distance(dashStartPosition, transform.position);
         if (distanceTraveled >= maxDashDistance || dashTimeElapsed >= dashDuration)
         {
            StopDash();
          }
        }
    }

    private void PlayerMovement()
    {
        if (controls.Player.Sprint. IsPressed()) 
        {
            Vector3 Sprint = (move.x * transform.right) + (move.y * transform.forward);                 
            controller.Move(Sprint * sprintSpeed * Time.deltaTime);
        }
        else
        {
            move = controls.Player.Movement.ReadValue<Vector2>();
            Vector3 movement = (move.y * transform.forward) + (move.x * transform.right);
            controller.Move(movement * moveSpeed * Time.deltaTime);
        }
        
    }

    private void Jump()
    {
		if (controls.Player.Jump.triggered && isGrounded()){
			velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
		}
    }

    void OnEnable()
    {
		controls.Enable();
    }

    void OnDisable()
    {
		controls.Disable();
    }
     void TriggerAction()
   {
       canTrigger = false;

       // Start the cooldown routine
       StartCoroutine(CooldownRoutine());
   }

   IEnumerator CooldownRoutine()
   {
       yield return new WaitForSeconds(CDT);
       canTrigger = true; // Flip the boolean flag (true becomes false, false becomes true)
   }

}
