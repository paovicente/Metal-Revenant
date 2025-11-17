using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private PlayerController playerController;

    [Header("Audio References")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip runClip;

    [Header("Audio Configurations")]
    [SerializeField] private float minPitch = 0.9f;
    [SerializeField] private float maxPitch = 1.2f;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.OnJumped += PlayJumpSound;
            playerController.OnRunning += PlayRunSound;
            playerController.OnStopRunning += StopRunLoop;
        }
    }

    private void PlayJumpSound()
    {

        if (LevelManager.instance != null && LevelManager.instance.isPaused)
            return;

        if (audioSource != null && jumpClip != null)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.PlayOneShot(jumpClip);
        }
    }

    private void PlayRunSound()
    {
        if (LevelManager.instance != null && LevelManager.instance.isPaused)
            return;

        if (audioSource != null && runClip != null)
        {
            audioSource.clip = runClip;
            audioSource.loop = true;
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.volume = 0.1f;
            audioSource.Play();
        }
    }

    private void StopRunLoop()
    {
        if (LevelManager.instance != null && LevelManager.instance.isPaused)
            return;

        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false;
            audioSource.volume = 1f;
        }
    }

    private void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.OnJumped -= PlayJumpSound;
            playerController.OnRunning -= PlayRunSound;
            playerController.OnStopRunning -= StopRunLoop;
        }
    }
    
}
