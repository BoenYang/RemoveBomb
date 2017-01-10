using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BackToLevelButton : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.ins.PlayButtonClick();
        GameManager.Ins.GameEnd();
        UIRootController.Ins.ShowUI(UIType.LevelUI,true,true);
    }
}
