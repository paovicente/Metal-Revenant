using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PlayerController player;

    [Header("Audio References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip runClip;

    [Header("Audio Configurations")]
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.2f;

    private bool wasRunning = false;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }

    private void Update()
    {
        HandleJumpSound();
        HandleRunSound();
    }

    private void HandleJumpSound()
    {
        // Si está subiendo y no estaba en el suelo justo antes → saltó
        if (player.IsJumping() && jumpClip != null)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(jumpClip);
        }
    }

    private void HandleRunSound()
    {
        bool isRunning = player.IsRunning();

        if (isRunning && !wasRunning)
        {
            audioSource.clip = runClip;
            audioSource.loop = true;
            audioSource.volume = 0.1f;
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.Play();
        }
        else if (!isRunning && wasRunning)
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.volume = 1f;
        }

        wasRunning = isRunning;
    }
}
