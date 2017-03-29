using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ResultView : UIBase {

	public Button BackToLevelView;

	public Button NextLevel;

	public Button Restart;

	public override void OnInit ()
	{
		BackToLevelView.onClick.AddListener (OnBackClick);
		NextLevel.onClick.AddListener (OnNextLevelClick);
		Restart.onClick.AddListener (OnRestartClick);
	}

	private void OnBackClick(){
		GameScene.Instance.Game.GameOver ();
		UIManager.ClosePanel ("GameView");
		ClosePanel ();
		SceneManager.LoadScene("MainScene");
	}

	private void OnNextLevelClick(){
	
	}

	private void OnRestartClick(){
		ClosePanel ();
		GameScene.Instance.Game.RestartGame ();
	}

}
