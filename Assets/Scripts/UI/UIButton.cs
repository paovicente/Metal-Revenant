using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private UIButtonSounds sound;
    [SerializeField] private bool isExitButton = false;

    private void Awake()
    {
        sound = GetComponent<UIButtonSounds>();
    }

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {

            if (isExitButton)
            {
                StartCoroutine(ExitAfterSound());
                return;
            }

            LevelManager.instance.LoadScene(sceneName, sound.clickSound.length);
        });
    }

    private IEnumerator ExitAfterSound()
    {
        if (sound != null && sound.clickSound != null)
            yield return new WaitForSeconds(sound.clickSound.length);

        LevelManager.instance.ExitGame();
    }
}
