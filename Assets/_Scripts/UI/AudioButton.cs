using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioButton : MonoBehaviour , IPointerClickHandler
{

    private Button button;

    public void OnPointerClick(PointerEventData eventData)
    {
    
        button = GetComponent<Button>();
        if (button.interactable == true)
        {
            button.interactable = false;
            AudioManager.ins.MuteAllAudio(true);
        }
        else
        {
            button.interactable = true;
            AudioManager.ins.MuteAllAudio(false);
        }
        AudioManager.ins.PlayButtonClick();
    }
}
