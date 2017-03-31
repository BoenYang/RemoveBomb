
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
		GlobalMng.GlobalSingleton<AudioMng> ().PlayMusic (MusicPath.Game);
        for (int i = 0; i < StarList.Count; i++)
        {
            StarList[i].HideStar();
        }
    }


    private void OnPauseClick()
    {
     
        if (GameScene.Instance.Game.GameRunning)
        {
            GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.Click);
            UIManager.OpenPanel("PauseView");
        }
    }

    private void OnRestartBtnClick()
    {
       
        if (GameScene.Instance.Game.GameRunning)
        {
            GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.Click);
            GameScene.Instance.Game.RestartGame();
        }
    }

    private void OnGetStar(UIMsg msg)
    {
        int startCount = (int) msg.args[0];
        GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.CatchStar + startCount);
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
