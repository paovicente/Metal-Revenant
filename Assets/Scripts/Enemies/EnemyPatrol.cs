using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private SpriteRenderer enemySpriteRenderer;

    [SerializeField] private float leftLimit = 6f;
    [SerializeField] private float rightLimit = 20f;

    private bool movingRight = false;
    private float targetLimit; //the limit enemy is moving toward

    public int damage = 5;

    private void Start()
    {
        targetLimit = leftLimit;
        enemySpriteRenderer.flipX = false;
    }

    private void Update()
    {
        float moveDir = movingRight ? 1f : -1f;
        transform.Translate(Vector3.right * moveDir * speed * Time.deltaTime);

        //check if we reached current target limit
        if ((movingRight && transform.position.x >= targetLimit) || (!movingRight && transform.position.x <= targetLimit))
        {
            //swap direction when reaching limit
            movingRight = !movingRight;
            targetLimit = movingRight ? rightLimit : leftLimit;
            enemySpriteRenderer.flipX = movingRight;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
                player.TakeDamage(damage);

            movingRight = !movingRight;

            targetLimit = movingRight ? rightLimit : leftLimit;
            enemySpriteRenderer.flipX = movingRight;
        }
    }
}
