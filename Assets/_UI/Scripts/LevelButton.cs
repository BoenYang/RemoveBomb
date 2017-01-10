using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour, IPointerClickHandler{

    public int Level = 1;

    public int Star;

    public bool Locked;

    public Text LevelText;

    private Image image;

    private Button button;

    private void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        UpdateButton();
    }

    public void UpdateButton()
    {
        if (Locked)
        {
            image.sprite = LevelManager.ins.LockSprite;
            LevelText.text = "";
        }
        else
        {
            LevelText.text = Level.ToString();
            image.sprite = LevelManager.ins.LevelButtonSprites[Star];
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.ins.PlayButtonClick();
        if (!Locked)
        {
            GameManager.Ins.Level = Level;
            UIRootController.Ins.ShowUI(UIType.GameUI,true,true);
            GameManager.Ins.GameStart();
        }
    }
}
