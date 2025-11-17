using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystem : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var systems = FindObjectsByType<EventSystem>(FindObjectsSortMode.None);
        foreach (var sys in systems)
        {
            if (sys.gameObject != this.gameObject)
                Destroy(sys.gameObject);
        }
    }
}
