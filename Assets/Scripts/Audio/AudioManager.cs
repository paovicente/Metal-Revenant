using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Mixer")]
    public AudioMixer masterMixer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            LoadVolumes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadVolumes()
    {
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);

        masterMixer.SetFloat("MusicVolume", Mathf.Log10(music) * 20f);
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(sfx) * 20f);
    }

    public void SetMusic(float value)
    {
        masterMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFX(float value)
    {
        masterMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20f);
        PlayerPrefs.SetFloat("SFXVolume", value);
    }
}
