using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ResetGameButton : MonoBehaviour, IPointerClickHandler{

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.ins.PlayButtonClick();
        switch (UIRootController.Ins.CurrentUI)
        {
            case UIType.GameUI:
                UIRootController.Ins.FadePanel.StartFade();
                GameManager.Ins.GameReset();
                break;
            case UIType.ResultUI:
            case UIType.PauseUI:
                UIRootController.Ins.Back(true);
                GameManager.Ins.GameReset();
                break;
        }
    }
}
