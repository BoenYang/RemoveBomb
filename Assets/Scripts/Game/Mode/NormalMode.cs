using System.Collections;
using UnityEngine;

public class NormalMode : GameModeBase
{
    public override string Mode
    {
        get { return "Normal"; }
    }

    private int LevelIndex = 1;

    private GameObject levelGo;

    private GameObject mapGo;

    public override void Init()
    {
        UIManager.OpenPanel("GameView", true);
    }

    public override void StartGame()
    {
		LoadLevel();
		base.StartGame ();
    }

    public override void RestartGame()
    {
        ClearLevel();
        StartGame();
    }

    public override void GameOver()
    {
		base.GameOver ();
		ClearLevel ();
    }

	public override void GameResult ()
	{
		base.GameResult ();
		UIManager.OpenPanel ("ResultView");
	}

    private void LoadLevel()
    {
        int package = LevelIndex / 16 + 1;

        GameObject levelObj = Resources.Load<GameObject>("level/Level" + LevelIndex);
        levelGo = GameObject.Instantiate(levelObj);
        levelGo.transform.localPosition = new Vector3(0, 0, 1);
        levelGo.transform.localScale = Vector3.one;

        GameObject mapObj = Resources.Load<GameObject>("Map/Map" + package);
        mapGo = GameObject.Instantiate(mapObj);
        mapGo.transform.localPosition = new Vector3(0, 0, 1);
        mapGo.transform.localScale = Vector3.one;
    }

    private void ClearLevel()
    {
        GameObject.Destroy(levelGo);
        GameObject.Destroy(mapGo);
    }

	protected override IEnumerator GameLoop ()
	{
		while (GameRunning) {
			GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
			if (bombs == null || bombs.Length == 0)
			{
				GameRunning = false;
				StartCoroutine(GameWinDelay(1F));
			}
			yield return new WaitForEndOfFrame ();
		}
	}

	private IEnumerator GameWinDelay(float s){
		yield return new WaitForSeconds(s);
		GameResult ();
	}
}
