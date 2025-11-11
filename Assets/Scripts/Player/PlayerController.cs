using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference dashAction;

    //Input animation
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    [Header("Inputs")]
    private Vector2 moveInput;

    [Header("Player variables")]
    [SerializeField] private float playerSpeed = 2f;
    [SerializeField] private float jumpForce = 8f;
    private BoxCollider2D playerCollider;
    private Vector3 playerPosition;

    [Header("Player states")]
    public bool isIdle = false;
    public bool isRunning = false;
    public bool isJumping = false;
    public bool isDashing = false;
    public bool isFalling = false;
    public bool isShooting = false;

    private bool wasRunning = false;

    public event Action OnJumped;
    public event Action OnRunning;
    public event Action OnStopRunning;

    [Header("Dash variables")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float lastDashTime = -10f;
    //Ver si esto puede andar el public Dash
  

    private void Start()
    {
        //This code separated into 2 different scripts one for rigidbody and another one for other actions
        //then in rigidbody controller script put a OnDisable and inside it disable the handle methods
        moveAction.action.started += HandleMoveInput;
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;

        jumpAction.action.started += HandleJumpInput;
        jumpAction.action.performed += HandleJumpInput;
        jumpAction.action.canceled += HandleJumpInput;

        dashAction.action.started += HandleDashInput;

        playerCollider = GetComponent<BoxCollider2D>();

    }

    private void FixedUpdate()
    {
        MovePlayer();
        
    }

    private void HandleMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void HandleJumpInput(InputAction.CallbackContext context) //the player will jump only if the player use the space bar and the feet are touching a platform or the floor
    {
        
        if (!isJumping && context.started)
        {
            playerRigidbody.linearVelocity = new Vector2(playerRigidbody.linearVelocity.x, jumpForce);
            isJumping = true;
            OnJumped?.Invoke(); //notify that the player is jumping
        
        isIdle = false;
        isRunning = false;
        }

    }

    private void MovePlayer()
    {
        Vector3 move = new Vector3(moveInput.x, 0); //the jump (move in y) will be managed separatedly
        transform.position += move * playerSpeed * Time.deltaTime;

        if (isDashing) return; //if it's dashing then do not change the velocity

        playerRigidbody.linearVelocity = new Vector2(moveInput.x * playerSpeed, playerRigidbody.linearVelocity.y); //moves in x according to the input and maintains velocity in y 

        bool isCurrentlyRunning = moveInput.x != 0;

        //when starts to move
        if (isCurrentlyRunning && !wasRunning)
        {
            isRunning = true;
            OnRunning?.Invoke();
        }
        else if (!isCurrentlyRunning && wasRunning) //when the player stops moving
        {
            isRunning = false;
            OnStopRunning?.Invoke();
        }
       
        wasRunning = isCurrentlyRunning;

        if (isJumping)//if isJumping then is not running or idle
        {
            isRunning = false;
            isIdle = false;
        }
        else if (moveInput.x != 0) //if the player is not jumping means that is on a platform or floor, and he moves then is running
        {
            isRunning = true;
            isIdle = false;
        }
        else //if is not jumping means that is on a platform or floor, and if he dont move then is idle
        {
            isRunning = false;
            isIdle = true;
        }
    }
        private void HandleDashInput(InputAction.CallbackContext context)
    {
        if (context.started && !isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(PerformDash());
        }
    }
    private IEnumerator PerformDash() 
    {
        isDashing = true;
        lastDashTime = Time.time;

        float dashDirection = Mathf.Sign(moveInput.x);
        if (dashDirection == 0) dashDirection = transform.localScale.x;

        float elapsed = 0f;
        while (elapsed < dashDuration)
        {
            playerRigidbody.linearVelocity = new Vector2(dashDirection * dashSpeed, 0f);
            elapsed += Time.deltaTime;

            Debug.Log("Dash");

            yield return null;
        }
        isDashing = false;

    }



}



