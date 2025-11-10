using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private UIButtonSounds sound;
    private void Awake()
    {
        sound = GetComponent<UIButtonSounds>();
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LevelManager.instance.LoadScene(sceneName, sound.clickSound.length);
        });
    }
}
