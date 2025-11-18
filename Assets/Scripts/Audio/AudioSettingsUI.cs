using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider playerSlider;

    private void Start()
    {
        musicSlider.value = AudioManager.instance.GetMusic();
        sfxSlider.value = AudioManager.instance.GetSFX();
        playerSlider.value = AudioManager.instance.GetPlayerSound();

        musicSlider.onValueChanged.AddListener(volume => AudioManager.instance.SetMusic(volume));
        sfxSlider.onValueChanged.AddListener(volume => AudioManager.instance.SetSFX(volume));
        playerSlider.onValueChanged.AddListener(volume => AudioManager.instance.SetPlayerSound(volume));
    }
}
