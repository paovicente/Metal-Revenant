using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")]
    public int pointsValue = 10;           // Cuántos puntos da al recogerlo
    public AudioClip collectSound;         // Sonido opcional
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (PlayerScore.Instance != null)
            {
                PlayerScore.Instance.AddPoints(pointsValue);
            }

            gameObject.SetActive(false);

            // Opcional: reproducir sonido
            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
        }
    }

}
