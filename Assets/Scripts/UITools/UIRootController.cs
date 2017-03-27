using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    MainUI,
    LevelUI,
    PauseUI,
    ResultUI,
    GameUI
}

[System.Serializable]
public class UI
{
    public UIType type;
    public GameObject go;
}

public class UIRootController : MonoBehaviour
{
    public static UIRootController Ins;

    public List<UI> UIs; 

    public Camera UICamera;

    public FadePanel FadePanel;

    private Stack<UI> UIStack = new Stack<UI>();

    private Dictionary<UIType, UI> UIDic = new Dictionary<UIType, UI>();

    public UIType CurrentUI {
        get {
            return UIStack.Peek().type;
        }
    }

    private void Awake()
    {
        Ins = this;
        foreach (var ui in UIs)
        {
            UIDic.Add(ui.type, ui);
        }
    }

    public UI[] arr;

    private void Update()
    {
        arr = UIStack.ToArray();
    }

    public void ShowUI(UIType type,bool fade = false, bool clear = false)
    {
        if (!fade)
        {
            Show(type,clear);
        }
        else
        {
            StartCoroutine(ShowUIWithFadeEffect(type,clear));
        }
    }

    public IEnumerator ShowUIWithFadeEffect(UIType type,bool clear)
    {
        FadePanel.gameObject.SetActive(true);
        FadePanel.StartFade();
        yield return new WaitForSeconds(FadePanel.fadeTime*0.5f);
        Show(type,clear);
    }

    private void Show(UIType type,bool clear){
        UI ui = UIDic[type];
        if (clear)
        {
            while (UIStack.Count != 0)
            {
                UI u = UIStack.Pop();
                u.go.SetActive(false);
            }
        }
   
        if (!ui.go.activeSelf)
        {
            ui.go.SetActive(true);
            UIStack.Push(ui);
        }
    }

    public void Back(bool fade = false)
    {
        if (!fade)
        {
            BackTo();
        }
        else {
            StartCoroutine(BackWithFadeEffect());
        }
    }

    private IEnumerator BackWithFadeEffect() {
        FadePanel.StartFade();
        yield return new WaitForSeconds(FadePanel.fadeTime * 0.5f);
        BackTo();
    }

    private void BackTo()
    {
        UI topUI = UIStack.Pop();
        topUI.go.SetActive(false);
        topUI = UIStack.Peek();
        topUI.go.SetActive(true);
    }

}
