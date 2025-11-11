using UnityEngine;

public class Animatorcontroller : MonoBehaviour
{
        [Header("Animator Reference")]    
    [SerializeField] private string isIdle = "isIdle";
    [SerializeField] private string isRunning = "isRunning";
    [SerializeField] private string isJumping = "isJumping";
    [SerializeField] private string isDashing = "isDashing";
  //[SerializeField] private string isFalling = "isFalling";
    [SerializeField] private string isShooting= "isShooting";


    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody2D playerRigidbody;

    private int isIdleHash;
    private int isRunningHash;
    private int isJumpingHash;
    private int isDashingHash;
   // private int isFallingHash;
    private int isShootingHash;

    private void Awake()
        {
         isIdleHash = Animator.StringToHash(isIdle);
         isRunningHash = Animator.StringToHash(isRunning);
         isJumpingHash = Animator.StringToHash(isJumping);
         isDashingHash = Animator.StringToHash(isDashing);
         //isFallingHash = Animator.StringToHash(isFalling);
         isShootingHash = Animator.StringToHash(isShooting);
        }

        public void UpdateAnimation(bool isRunning, bool isIdle, bool isJumping, bool isDashing,bool isShooting /*bool isFalling*/)
        {
        /*
            if (PlayerAttack != null && PlayerAttack.Is_attacking)
            {
                animator.SetBool(Is_attackingHash, true);
                animator.SetBool(Is_movingHash, false);
                animator.SetBool(Is_idleHash, false);
                animator.SetBool(Is_jumpingHash, false);
                animator.SetBool(Is_fallingHash, false);
                return;
            }*/
            animator.SetBool(isRunningHash, isRunning);
            animator.SetBool(isIdleHash, isIdle);
            animator.SetBool(isDashingHash, isDashing);
            animator.SetBool(isJumpingHash, isJumping);
        //animator.SetBool(isFallingHash, isFalling);

        // bool isFalling = playerRigidbody.linearVelocity.y < -0.1f && !isJumping && !isRunning;
        // animator.SetBool(isFallingHash, isFalling);
    }

   }
