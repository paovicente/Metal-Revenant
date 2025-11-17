using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 5f;
    private float lifeTime = 2f;

    [SerializeField] private int damage = 10;

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
                player.TakeDamage(damage);
        }
        Deactivate();
    }

    private void Deactivate()
    {
        CancelInvoke();
        gameObject.SetActive(false);
    }
}

