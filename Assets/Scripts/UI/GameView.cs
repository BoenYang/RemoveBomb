
using UnityEngine.UI;

public class GameView : UIBase
{

    public Button PauseBtn;

    public Button RestartBtn;

	public override void OnInit ()
	{
		PauseBtn.onClick.AddListener(OnPauseClick);
        RestartBtn.onClick.AddListener(OnRestartBtnClick);
	}

    private void OnPauseClick()
    {
        UIManager.OpenPanel("PauseView");
    }

    private void OnRestartBtnClick()
    {
        GameScene.Instance.Game.RestartGame();
    }

}
