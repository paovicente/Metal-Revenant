using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float speed = 14f;
    public float lifeTime = 2f;

    private Rigidbody2D rb;
    private float lifeTimer;
    private Vector2 pendingDirection;
    private bool hasPendingDirection = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0f;
            rb.freezeRotation = true;
        }

        // Ignorar colisión con el jugador
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Collider2D playerCollider = player.GetComponent<Collider2D>();
            Collider2D bulletCollider = GetComponent<Collider2D>();
            if (playerCollider != null && bulletCollider != null)
                Physics2D.IgnoreCollision(bulletCollider, playerCollider);
        }
    }


    private void OnEnable()
    {
        lifeTimer = 0f;

        // Si se estableci� una direcci�n antes de activar: aplicarla
        if (hasPendingDirection)
        {
            rb.linearVelocity = pendingDirection * speed;
            hasPendingDirection = false;
        }
    }

    private void Update()
    {
        lifeTimer += Time.deltaTime;
        if (lifeTimer >= lifeTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ignorar colisi�n con el player si hace falta (agregar tag check)
        // if (other.CompareTag("Player")) return;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Llama para disparar la bala desde afuera. Funciona tanto si la bala ya est� activa (aplica velocidad inmediatamente)
    /// como si est� inactiva (guarda la direcci�n y la aplicar� en OnEnable).
    /// </summary>
    public void Fire(Vector2 direction)
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
            if (rb == null) return;
        }

        if (gameObject.activeInHierarchy)
        {
            rb.linearVelocity = direction * speed;
            hasPendingDirection = false;
        }
        else
        {
            // Guardamos la direcci�n y la aplicamos en OnEnable
            pendingDirection = direction;
            hasPendingDirection = true;
        }
    }
}
