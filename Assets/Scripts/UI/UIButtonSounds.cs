using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSounds : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
//implements interfaces which forces this class to implement their methods
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

