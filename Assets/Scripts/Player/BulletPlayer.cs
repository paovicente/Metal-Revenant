using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifeTime = 2f;

    private float lifeTimer;

    private void OnEnable()
    {
        lifeTimer = 0f;
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
        {
            BulletPoolPlayer.Instance.ReturnBullet(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            //collision.GetComponent<EnemyHealth>()?.TakeDamage(1);

            BulletPoolPlayer.Instance.ReturnBullet(gameObject);
        }
    }
}
