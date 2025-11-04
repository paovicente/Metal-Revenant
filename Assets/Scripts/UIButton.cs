using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            LevelManager.instance.LoadScene(sceneName);
        });
    }
}
