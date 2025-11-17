using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 6f;
    private Vector2 direction;

    private int damage = 10;

    public void Shoot(Vector2 dir)
    {
        direction = dir.normalized;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) > 40 || Mathf.Abs(transform.position.y) > 40)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {

            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
                playerHealth.TakeDamage(damage); 

            gameObject.SetActive(false);
        }

    }
}
