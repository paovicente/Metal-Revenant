using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [Header("Asignar en Inspector")]
    public RuntimeAnimatorController armedAnimator; // animator del jugador cuando tiene arma

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        PlayerShoot shooter = collision.GetComponent<PlayerShoot>();
        Animator anim = collision.GetComponent<Animator>();

        if (shooter != null)
            shooter.enabled = true;          // activa el disparo del jugador

        if (anim != null && armedAnimator != null)
            anim.runtimeAnimatorController = armedAnimator; // cambia animator

        // destruir arma del piso
        Destroy(gameObject);
    }
}
