/*using System.Collections;
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
}*/
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] private InputActionReference startAction;
    [SerializeField] private string startScreenScene = "StartScreen";
    [SerializeField] private string menuScene = "Menu";
    [SerializeField] private Camera fallbackCamera;
    private Scene activeScene;

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
    }

    private void GoToMenu(InputAction.CallbackContext context)
    {
        if (SceneManager.GetActiveScene().name == startScreenScene)
        {
            StartCoroutine(TransitionToMenu());
            startAction.action.Disable();
        }
    }

    private IEnumerator TransitionToMenu()
    {
        var scene = SceneManager.LoadSceneAsync(menuScene, LoadSceneMode.Additive);
        
        yield return scene; //waits until the scene is completely loaded

        activeScene = SceneManager.GetSceneByName(menuScene);
        SceneManager.SetActiveScene(activeScene);

        yield return SceneManager.UnloadSceneAsync(startScreenScene);

    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneDelayed(sceneName));
    }

    private IEnumerator LoadSceneDelayed(string sceneName)
    {
        //yield return new WaitForSeconds(0.2f);
        
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
}

