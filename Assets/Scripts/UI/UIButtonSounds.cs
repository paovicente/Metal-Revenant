using UnityEngine;
using UnityEngine.EventSystems;

/// <summary /> implements interfaces which forces this class to implement their methods
public class UIButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public AudioClip hoverSound;
    public AudioClip clickSound;

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIAudioManager.instance.PlaySound(hoverSound);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UIAudioManager.instance.PlaySound(clickSound);
    }
}

