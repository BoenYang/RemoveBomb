using System.Collections.Generic;
using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.UI;

public class GameView : UIBase
{

    public Button PauseBtn;

    public Button RestartBtn;

    public List<ResultStar> StarList;

    public Image CutLine;

    private Vector3 startPos;

    private Vector2 screenSize;

    private Vector2 panelSize;


	public override void OnInit ()
	{
		PauseBtn.onClick.AddListener(OnPauseClick);
        RestartBtn.onClick.AddListener(OnRestartBtnClick);

        AddMsgListener("GetStar",OnGetStar);
        AddMsgListener("ResetGame",OnResetGame);
        AddMsgListener("TouchDown",OnTouchDown);
        AddMsgListener("TouchMove",OnTouchMove);
        AddMsgListener("TouchUp",OnTouchUp);
        AddMsgListener("GetResult", OnGameResult);
        CutLine.gameObject.SetActive(false);

	    screenSize = new Vector2(Screen.width,Screen.height);
	    panelSize = GetComponent<RectTransform>().sizeDelta;
	}

    public override void OnRefresh()
    {
        if (AdmobTools.Banner.AdPos != AdPosition.Bottom)
        {
            AdmobTools.Banner.RequestBanner();
        }

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

    private void OnTouchDown(UIMsg msg)
    {
        CutLine.gameObject.SetActive(true);
        startPos = (Vector3)msg.args[0];

        Vector3 viewPos = UIManager.UICamera.ScreenToViewportPoint(startPos) - new Vector3(0.5f,0.5f,0);

        startPos.x =  panelSize.x*viewPos.x ;
        startPos.y = panelSize.y*viewPos.y;

        Debug.Log("点击位置 " + startPos + " 视口位置 " + viewPos);

        CutLine.rectTransform.anchoredPosition = startPos;
    }

    private void OnTouchMove(UIMsg msg)
    {
        Vector3 pos = (Vector3)msg.args[0];
        Vector3 viewPos = UIManager.UICamera.ScreenToViewportPoint(pos) - new Vector3(0.5f, 0.5f, 0);
        pos.x = panelSize.x * viewPos.x;
        pos.y = panelSize.y * viewPos.y;

        float distance = Vector2.Distance(startPos, pos);
        float fillAmount = distance/1500;
        CutLine.fillAmount = fillAmount;

        Vector2 dir = pos - startPos;
        float angle = -Vector2.Angle(dir, Vector2.right);
        if (startPos.y < pos.y)
        {
            angle = -angle;
        }

        CutLine.rectTransform.localEulerAngles = new Vector3(0, 0, angle);
    }

    private void OnTouchUp(UIMsg msg)
    {
        CutLine.gameObject.SetActive(false);
    }

    private void OnGameResult(UIMsg msg)
    {
        CutLine.gameObject.SetActive(false);
    }
}
