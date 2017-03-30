
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
        GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.Click);
        GameScene.Instance.Game.GameOver();
		UIManager.ClosePanel ("GameView");
        ClosePanel();
        SceneManager.LoadScene("MainScene");
    }

    private void OnContinueClick()
    {
        GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.Click);
        GameScene.Instance.Game.ResumeGame();
        ClosePanel();
    }

    private void OnRestartClick()
    {
        GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.Click);
        ClosePanel();
        GameScene.Instance.Game.RestartGame();
    }
}
