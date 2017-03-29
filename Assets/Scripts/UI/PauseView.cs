
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : UIBase
{
    public Button BackToLevelBtn;

    public Button ContinueBtn;

    public Button RestartBtn;

    public Button ShopBtn;

    public Toggle SoundToggle;

    public override void OnInit()
    {
        BackToLevelBtn.onClick.AddListener(OnBackToLevelClick);
        ContinueBtn.onClick.AddListener(OnContinueClick);
        RestartBtn.onClick.AddListener(OnRestartClick);
    }


    public override void OnRefresh()
    {
        GameScene.Instance.Game.PauseGame();
    }

    private void OnBackToLevelClick()
    {
        GameScene.Instance.Game.GameOver();
        ClosePanel();
        SceneManager.LoadScene("MainScene");
    }

    private void OnContinueClick()
    {
        GameScene.Instance.Game.PauseGame();
        ClosePanel();
    }

    private void OnRestartClick()
    {
        ClosePanel();
        GameScene.Instance.Game.RestartGame();
    }
}
