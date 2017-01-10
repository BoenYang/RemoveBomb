using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour , IPointerClickHandler{

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.ins.PlayButtonClick();
        UIRootController.Ins.ShowUI(UIType.LevelUI,true,true);
    }
}
