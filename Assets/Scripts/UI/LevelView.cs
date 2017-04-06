using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using GoogleMobileAds.Api;

public class LevelView : UIBase {

	public Button BackBtn;

    public Button ShopBtn;

	public Transform TittleRoot;

	public Transform LevelItemRoot;

	public List<LevelButton> LevelBtns = new List<LevelButton>();

	public Vector2 StartPos;

	public float VerticalSpacing  = 100f;

	public float HorizontalSpacing = 100f;

    public LevelScroll PageView;

	private GameObject levelBtnObj = null;

	public override void OnInit ()
	{
		BackBtn.onClick.AddListener (OnBackClick);

	    ShopBtn.interactable = false;

		levelBtnObj = Resources.Load<GameObject> ("UITools/LevelBtn");

		Vector3 startPos = Vector3.zero + new Vector3 (- HorizontalSpacing * 2, VerticalSpacing * 2) + new Vector3(StartPos.x,StartPos.y,0f);

		for (int i = 0; i < 32; i++) {

			int col = i % 4;
			int row = i / 4;
			row = row%4;
		    int packageIndex = i/16;

			GameObject levelGo = Instantiate (levelBtnObj);
			levelGo.name = "Level" + i;
			levelGo.transform.SetParent (LevelItemRoot);
			levelGo.transform.localPosition = startPos + new Vector3(col * HorizontalSpacing + packageIndex * PageView.PageDistance, - row * VerticalSpacing);
			levelGo.transform.localScale = Vector3.one;

			LevelButton level = levelGo.GetComponent<LevelButton>();
			level.Init ();
            LevelBtns.Add(level);
        }
	}

	public override void OnRefresh ()
	{
	    if (AdmobTools.Banner.AdPos != AdPosition.Top)
	    {
            AdmobTools.Banner.RequestBanner(null,AdPosition.Top);
        }

	    GlobalMng.GlobalSingleton<AudioMng> ().PlayMusic (MusicPath.Background);
	    for (int i = 0; i < LevelBtns.Count; i++)
	    {
	        LevelBtns[i].UpdateBtnState(i+1);
	    }

	    int pageIndex = (PlayerInfo.CurrentPlayer.CurrentLevelIndex - 1)/16 + 1;
        PageView.SetShowPage(pageIndex);

	}

	private void OnBackClick(){
        GlobalMng.GlobalSingleton<AudioMng>().PlaySound(MusicPath.Click);
        UIManager.CloseTop ();
	}
}
