using UnityEngine;

public class Animatorcontroller : MonoBehaviour
{
    [Header("Animator Parameters (names in Animator)")]
    [SerializeField] private string isIdle = "isIdle";
    [SerializeField] private string isRunning = "isRunning";
    [SerializeField] private string isJumping = "isJumping";
    [SerializeField] private string isDashing = "isDashing";
    [SerializeField] private string isFalling = "isFalling";

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D playerRigidbody;

    private int isIdleHash;
    private int isRunningHash;
    private int isJumpingHash;
    private int isDashingHash;
    private int isFallingHash;

    private void Awake()
    {
        isIdleHash = Animator.StringToHash(isIdle);
        isRunningHash = Animator.StringToHash(isRunning);
        isJumpingHash = Animator.StringToHash(isJumping);
        isDashingHash = Animator.StringToHash(isDashing);
        isFallingHash = Animator.StringToHash(isFalling);
    }

    /// <summary>
    /// Receive the states calculated by the PlayerController
    /// </summary>
    public void UpdateAnimation(
        bool isRunning,
        bool isIdle,
        bool isJumping,
        bool isDashing,
        bool isFalling)
    {
        animator.SetBool(isRunningHash, isRunning);
        animator.SetBool(isIdleHash, isIdle);
        animator.SetBool(isJumpingHash, isJumping);
        animator.SetBool(isDashingHash, isDashing);
        animator.SetBool(isFallingHash, isFalling);
    }
}

