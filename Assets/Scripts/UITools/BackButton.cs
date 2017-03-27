using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour , IPointerClickHandler{

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.ins.PlayButtonClick();
        switch (UIRootController.Ins.CurrentUI)
        {
              case UIType.LevelUI:
                UIRootController.Ins.ShowUI(UIType.MainUI, true, true);
                break;
              case UIType.PauseUI:
                UIRootController.Ins.Back(false);
                break;
        }
    }
}
