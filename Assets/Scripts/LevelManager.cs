using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private InputActionReference startAction;
    [SerializeField] private InputActionReference pauseAction;

    [SerializeField] private string startScreenScene = "StartScreen";
    [SerializeField] private string menuScene = "Menu";
    [SerializeField] private string pauseScene = "PauseMenu";
    [SerializeField] private Camera fallbackCamera;
    private Scene activeScene;

    private bool isPaused = false;

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
            return;
        }

        //when the game starts, this load the start screen instantly
        SceneManager.LoadSceneAsync(startScreenScene, LoadSceneMode.Additive);
       
    }

    private void Start()
    {
        activeScene = SceneManager.GetSceneByName(startScreenScene);
        SceneManager.SetActiveScene(activeScene);

        if (startAction != null)
        {
            startAction.action.Enable();
            startAction.action.performed += GoToMenu;
        }
        
        if (pauseAction != null)
        {
            pauseAction.action.Enable();
            pauseAction.action.performed += GoToPause;
        }
    }

    private void GoToMenu(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name == startScreenScene)
        {
            LoadScene(menuScene);
            startAction.action.Disable();
        }
    }

    private void GoToPause(InputAction.CallbackContext context)
    {
        string currentScene = SceneManager.GetActiveScene().name;

        if (!currentScene.Contains("Level") || currentScene.Contains("Selector"))
            return;

        if (!isPaused)
            StartCoroutine(LoadPauseMenu());
        else
            ResumeGame();
    }

    public void LoadScene(string sceneName, float delay = 0.2f)
    {
        StartCoroutine(LoadSceneDelayed(sceneName, delay));
    }

    private IEnumerator LoadSceneDelayed(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        
        Scene previousScene = SceneManager.GetActiveScene();

        //load the new scene
        var scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        yield return scene;

        if (fallbackCamera != null)
            fallbackCamera.enabled = false;

        activeScene = SceneManager.GetSceneByName(sceneName);
        SceneManager.SetActiveScene(activeScene);

        
        yield return SceneManager.UnloadSceneAsync(previousScene);   
    }

    private IEnumerator LoadPauseMenu()
    {
        isPaused = true;
        Time.timeScale = 0f;

        yield return SceneManager.LoadSceneAsync(pauseScene, LoadSceneMode.Additive);
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;

        SceneManager.UnloadSceneAsync(pauseScene);
    }
}

