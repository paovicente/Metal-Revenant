using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume",1f);
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 1f);

        musicSlider.onValueChanged.AddListener((volume) => {
            AudioManager.instance.SetMusic(volume);
        });

        sfxSlider.onValueChanged.AddListener((volume) => {
            AudioManager.instance.SetSFX(volume);
        });
    }
}
