using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ContinueButton : MonoBehaviour, IPointerClickHandler {

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.ins.PlayButtonClick();
        UIRootController.Ins.Back();
        GameManager.Ins.GameResume();
    }
}
