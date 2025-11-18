using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer sprite;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 60f;

    private float moveInput;
    private float currentSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;

    private bool isGrounded;
    private float originalGravity;

    [Header("Better Jump")]
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashTime = 0.15f;
    [SerializeField] private float dashCooldown = 0.5f;

    private bool isDashing;
    private float lastDashTime = -10f;

    private void Start()
    {
        originalGravity = rb.gravityScale;
    }

    private void Update()
    {
        ReadInputs();
        CheckGround();
        ApplyBetterJump();
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
            Move();
    }

    // --------------------------------------------------
    // INPUTS
    // --------------------------------------------------
    private void ReadInputs()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            TryDash();
    }

    // --------------------------------------------------
    // MOVEMENT
    // --------------------------------------------------
    private void Move()
    {
        float targetSpeed = moveInput * moveSpeed;

        if (Mathf.Abs(targetSpeed) > 0.1f)
            currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.fixedDeltaTime);
        else
            currentSpeed = Mathf.MoveTowards(currentSpeed, 0, deceleration * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);

        if (moveInput != 0)
            sprite.flipX = moveInput < 0;
    }

    // --------------------------------------------------
    // JUMP
    // --------------------------------------------------
    private void Jump()
    {
        if (!isGrounded || isDashing) return;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    // --------------------------------------------------
    // BETTER JUMP (makes jumps feel tight and responsive)
    // --------------------------------------------------
    private void ApplyBetterJump()
    {
        if (isDashing) return;

        // Caída más rápida
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // Salto corto (si soltás la tecla de salto)
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    // --------------------------------------------------
    // DASH
    // --------------------------------------------------
    private void TryDash()
    {
        if (Time.time < lastDashTime + dashCooldown) return;
        if (isDashing) return;

        // Leer input de dash (Horizontal y Vertical)
        float dashX = Input.GetAxisRaw("Horizontal");
        float dashY = Input.GetAxisRaw("Vertical");

        // Si no hay input, dash hacia adelante según el sprite
        if (Mathf.Approximately(dashX, 0f) && Mathf.Approximately(dashY, 0f))
            dashX = sprite.flipX ? -1f : 1f;

        Vector2 dashDirection = new Vector2(dashX, dashY).normalized;

        StartCoroutine(DashRoutine(dashDirection));
    }

    private System.Collections.IEnumerator DashRoutine(Vector2 direction)
    {
        isDashing = true;
        lastDashTime = Time.time;

        rb.gravityScale = 0;
        rb.linearVelocity = direction * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }



    private System.Collections.IEnumerator DashRoutine()
    {
        isDashing = true;
        lastDashTime = Time.time;

        float direction = sprite.flipX ? -1 : 1;

        rb.gravityScale = 0;
        rb.linearVelocity = new Vector2(direction * dashSpeed, 0);

        yield return new WaitForSeconds(dashTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
    }

    // --------------------------------------------------
    // ANIMATOR
    // --------------------------------------------------
    private void UpdateAnimator()
    {
        bool running = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool jumping = rb.linearVelocity.y > 0.1f && !isGrounded;
        bool falling = rb.linearVelocity.y < -0.1f && !isGrounded;
        bool idle = !running && isGrounded && !isDashing;

        animator.SetBool("isIdle", idle);
        animator.SetBool("isRunning", running);
        animator.SetBool("isJumping", jumping);
        animator.SetBool("isFalling", falling);
        animator.SetBool("isDashing", isDashing);
    }

    public bool IsRunning()
    {
        return Mathf.Abs(rb.linearVelocity.x) > 0.1f && isGrounded && !isDashing;
    }

    public bool IsJumping()
    {
        return rb.linearVelocity.y > 0.1f && !isGrounded;
    }


    // --------------------------------------------------
    // GIZMOS
    // --------------------------------------------------
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
