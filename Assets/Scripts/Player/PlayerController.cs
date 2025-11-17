using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference dashAction;

    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private Animatorcontroller playerAnimator;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;

    [Header("Ground Check")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Vector2 groundCheckRadius = new Vector2(0.8f, 0.1f);
    [SerializeField] private Vector2 groundBoxOffset = new Vector2(0f, -0.5f);

    [Header("Player Variables")]
    [SerializeField] private float playerSpeed = 2f;
    [SerializeField] private float jumpForce = 8f;

    public event Action OnJumped;
    public event Action OnRunning;
    public event Action OnStopRunning;

    public bool isIdle;
    public bool isRunning;
    public bool isJumping;
    public bool isFalling = false;
    public bool isDashing;
    private bool wasRunning = false;

    private bool isGrounded;
    private Vector2 moveInput;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 10f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private float lastDashTime = -10f;

    private void Start()
    {
        moveAction.action.started += HandleMoveInput;
        moveAction.action.performed += HandleMoveInput;
        moveAction.action.canceled += HandleMoveInput;

        jumpAction.action.started += HandleJumpInput;

        dashAction.action.started += HandleDashInput;
    }

    private void OnEnable()
    {
        jumpAction.action.started += HandleJumpInput;
    }

    private void OnDisable()
    {
        jumpAction.action.started -= HandleJumpInput;
    }


    private void Update()
    {
        GroundCheck();
        UpdateStates();

        playerAnimator.UpdateAnimation(
            isRunning,
            isIdle,
            isJumping,
            isDashing,
            isFalling
        );
    }

    /*MovePlayer() is called in FixedUpdate because Rigidbody movement must run in the physics loop.
    This ensures smooth. We skip it during dash to avoid overriding dash velocity*/

    private void FixedUpdate()
    {
        if (!isDashing)
            MovePlayer();
    }

    // ----------- INPUT FUNCTIONS -----------

    private void HandleMoveInput(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        playerSpriteRenderer.flipX = moveInput.x < 0;
    }

    private void HandleJumpInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (isGrounded)
        {
            playerRigidbody.linearVelocity =
                new Vector2(playerRigidbody.linearVelocity.x, jumpForce);

            isJumping = true;
            isFalling = false;

            OnJumped?.Invoke();
        }
    }

    /// <summary>
    /// Allow dash only if not currently dashing and the cooldown has fully elapsed.
    /// </summary>
    private void HandleDashInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        if (!isDashing && Time.time >= lastDashTime + dashCooldown)
        {
            StartCoroutine(PerformDash());
        }
    }

    // ----------- MOVEMENT -----------

    private void MovePlayer()
    {
        playerRigidbody.linearVelocity =
            new Vector2(moveInput.x * playerSpeed, playerRigidbody.linearVelocity.y);
    }

    // ----------- DASH -----------

    private IEnumerator PerformDash()
    {
        isDashing = true;
        lastDashTime = Time.time;

        float direction = Mathf.Sign(moveInput.x);
        if (direction == 0)
            direction = transform.localScale.x;

        float elapsed = 0f;
        while (elapsed < dashDuration)
        {
            playerRigidbody.linearVelocity = new Vector2(direction * dashSpeed, 0f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        isDashing = false;
    }

    /// <summary>
    /// Checks if the player is grounded using an OverlapBox under the collider
    /// Updates isGrounded, and sets falling/jumping states based on vertical velocity
    /// </summary>
    private void GroundCheck()
    {
        BoxCollider2D col = GetComponent<BoxCollider2D>();

        Vector2 boxCenter = (Vector2)transform.position + new Vector2(0, -col.bounds.extents.y - 0.05f);

        Vector2 boxSize = new Vector2(col.bounds.size.x * 0.9f, 0.1f);

        isGrounded = Physics2D.OverlapBox(
            boxCenter,
            boxSize,
            0,
            groundLayer
        );

        Debug.Log("isGrounded: " + isGrounded);

        if (isGrounded)
        {
            if (!isJumping)
                isFalling = false;
        }

        if (!isGrounded && playerRigidbody.linearVelocity.y < -0.1f)
        {
            isFalling = true;
            isJumping = false;
        }
    }

    private void UpdateStates()
    {
        bool runningNow = moveInput.x != 0 && isGrounded;

        if (runningNow && !wasRunning)
            OnRunning?.Invoke();

        if (!runningNow && wasRunning)
            OnStopRunning?.Invoke();

        wasRunning = runningNow;

        isRunning = runningNow;
        isIdle = moveInput.x == 0 && isGrounded && !isJumping;

        if (isFalling) isRunning = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 boxCenter = (Vector2)transform.position + new Vector2(0, -0.45f);
        Vector2 boxSize = new Vector2(0.7f, 0.1f);

        Gizmos.DrawWireCube(boxCenter, boxSize);
    }

}
