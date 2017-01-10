using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NextLevelButton : MonoBehaviour , IPointerClickHandler{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.ins.PlayButtonClick();
        UIRootController.Ins.Back(true);
        GameManager.Ins.NextLevel();
    }
}
