using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PauseButton : MonoBehaviour, IPointerClickHandler
{

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.ins.PlayButtonClick();
        UIRootController.Ins.ShowUI(UIType.PauseUI,false,false);
        GameManager.Ins.GamePause();
    }
}
