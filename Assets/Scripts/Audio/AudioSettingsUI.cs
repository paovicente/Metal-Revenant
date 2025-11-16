using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.value = AudioManager.instance.GetMusic();
        sfxSlider.value = AudioManager.instance.GetSFX();

        musicSlider.onValueChanged.AddListener(volume => AudioManager.instance.SetMusic(volume));
        sfxSlider.onValueChanged.AddListener(volume => AudioManager.instance.SetSFX(volume));
    }
}
