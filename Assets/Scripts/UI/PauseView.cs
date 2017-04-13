using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseView : UIBase
{
    public Button BackToLevelBtn;

    public Button ContinueBtn;

    public Button RestartBtn;

    public Button ShopBtn;

    public Toggle SoundToggle;

	private bool musicOn;

    public override void OnInit()
    {
        BackToLevelBtn.onClick.AddListener(OnBackToLevelClick);
        ContinueBtn.onClick.AddListener(OnContinueClick);
        RestartBtn.onClick.AddListener(OnRestartClick);

		SoundToggle.onValueChanged.AddListener (OnAudioToggleValChange);

        ShopBtn.interactable = false;

    }


    public override void OnRefresh()
    {
        GameScene.Instance.Game.PauseGame();
    }

    private void OnBackToLevelClick()
    {
		musicOn = PlayerPrefs.GetInt ("MusicOn",1) == 1;
		SoundToggle.isOn = !musicOn;
        GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.Click);
        GameScene.Instance.Game.GameOver();
		UIManager.ClosePanel ("GameView");
        UIManager.CloseTop();
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

	private void OnAudioToggleValChange(bool val){
		GlobalMng.GlobalSingleton<AudioMng> ().MusicOn = !val;
		GlobalMng.GlobalSingleton<AudioMng> ().SoundOn = !val;
		musicOn = !val;
		PlayerPrefs.SetInt ("MusicOn", !val ? 1 : 0);
	}
}
