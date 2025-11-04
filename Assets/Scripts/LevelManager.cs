using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private InputActionReference startAction;
    [SerializeField] private string sceneName = "Menu";//first scene to be loaded after the StartScreen

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startAction.action.Enable();
        startAction.action.performed += GoToMenu;
    }

    private void GoToMenu(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name == "StartScreen")
        {
            StartCoroutine(LoadSceneDelayed(sceneName));
            startAction.action.Disable();
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneDelayed(sceneName));
    }

    //this was done to first play the click sound when press a button and then load the corresponding scene
    private IEnumerator LoadSceneDelayed(string sceneName)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneName); 
    }
}
