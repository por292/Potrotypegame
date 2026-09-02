using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private CharacterInput controls; // Our controls we defined
    private Vector3 velocity; // To hold our current velocity
    private Vector2 move; // To hold our move
    

    private CharacterController controller; // Character controller

    public float moveSpeed = 6f; // To calibrate our movement
    public float jumpHeight = 2.4f; // To calibrate our jump
    public float gravity = -9.81f; // To calibrate our gravity
    public float sprintSpeed = 10f;
    public float dashing = 10f;
    [SerializeField] private float CDT = 2f;
    public bool canTrigger = true;
    public Transform ground; // Ground check empty goes in here
    public float distanceToGround = 0.4f; // How close the ground needs to be to register
    public LayerMask groundMask; // What layer is ground?

    public float dashSpeed = 20f;
    public float dashDuration = 0.2f;
    private bool isDashing = false;
    private Vector3 dashDirection;
    private Vector3 dashStartPosition;
    private float dashTimeElapsed;
    public float playerSlide;

    void Awake()
    {
		controls = new CharacterInput();
		controller = GetComponent<CharacterController>();
    }

    void Update()
    {
		PlayerMovement();
		Grav();
		Jump();
        //Dashing();
        //TriggerAction();
        if (controls.Player.Dash.IsPressed())
        {
            StartDash();
        }

        if (isDashing)
        {
            Dashing();
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
         //TriggerAction();
         //StartDash();
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
        if (controls.Player.Sprint.IsPressed()) 
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
        Dashing();
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
    void slide()
    {

    }
}
