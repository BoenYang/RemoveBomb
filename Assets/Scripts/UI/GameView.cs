
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : UIBase
{

    public Button PauseBtn;

    public Button RestartBtn;

    public List<ResultStar> StarList;

	public override void OnInit ()
	{
		PauseBtn.onClick.AddListener(OnPauseClick);
        RestartBtn.onClick.AddListener(OnRestartBtnClick);

        AddMsgListener("GetStar",OnGetStar);
        AddMsgListener("ResetGame",OnResetGame);
	}

    public override void OnRefresh()
    {
        for (int i = 0; i < StarList.Count; i++)
        {
            StarList[i].HideStar();
        }
    }


    private void OnPauseClick()
    {
        if (GameScene.Instance.Game.GameRunning)
        {
            UIManager.OpenPanel("PauseView");
        }
    }

    private void OnRestartBtnClick()
    {
        if (GameScene.Instance.Game.GameRunning)
        {
            GameScene.Instance.Game.RestartGame();
        }
    }

    private void OnGetStar(UIMsg msg)
    {
        int startCount = GameScene.Instance.GetGameMode<NormalMode>().StarCount;
        StarList[startCount - 1].ShowStar();
    }

    private void OnResetGame(UIMsg msg)
    {
        for (int i = 0; i < StarList.Count; i++)
        {
            StarList[i].HideStar();
        }
    }
}
