using UnityEngine;

public class FireRatePowerup : MonoBehaviour
{
    [SerializeField] private float duration = 8f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShoot shoot = collision.GetComponent<PlayerShoot>();
            if (shoot != null)
            {
                shoot.ActivateFireRatePowerup(duration);
            }

            gameObject.SetActive(false);
        }
    }
}
