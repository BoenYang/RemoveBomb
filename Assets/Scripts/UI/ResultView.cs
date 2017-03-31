using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ResultView : UIBase {

	public Button BackToLevelView;

	public Button NextLevel;

	public Button Restart;

    public List<ResultStar> StarList; 

    private int starCount;

    private bool isAnimating;

    private int currentLevelIndex;

	public override void OnInit ()
	{
		BackToLevelView.onClick.AddListener (OnBackClick);
		NextLevel.onClick.AddListener (OnNextLevelClick);
		Restart.onClick.AddListener (OnRestartClick);
	}

    public override void OnRefresh()
    {
        starCount = (int)Args[0];
        currentLevelIndex = (int) Args[1];
        StarList.ForEach((s) => s.HideStar());
        StartCoroutine(ShowStarEffect());

        if (currentLevelIndex >= 32)
        {
            NextLevel.gameObject.SetActive(false);
        }
        else
        {
            NextLevel.gameObject.SetActive(true);
        }
    }

    private IEnumerator ShowStarEffect()
    {
        isAnimating = true;
        for (int i = 0; i < starCount; i++)
        {
            StarList[i].ShowStar();
            yield return new WaitForSeconds(0.5f);
        }
        isAnimating = false;
    }

    private void OnBackClick(){

        if (isAnimating)
        {
            return;
        }

        GameScene.Instance.Game.GameOver ();
		UIManager.ClosePanel ("GameView");
		ClosePanel ();
		SceneManager.LoadScene("MainScene");
	}

	private void OnNextLevelClick()
    {
        if (isAnimating)
        {
            return;
        }

        ClosePanel();
        GameScene.Instance.GetGameMode<NormalMode>().NextLevel();
        GameScene.Instance.Game.RestartGame();
	}

	private void OnRestartClick(){

        if (isAnimating)
        {
            return;
        }

        ClosePanel ();
		GameScene.Instance.Game.RestartGame ();
	}

}
