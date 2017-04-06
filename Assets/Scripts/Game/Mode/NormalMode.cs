using System.Collections;
using UnityEngine;

public class NormalMode : GameModeBase
{
    public override string Mode
    {
        get { return "Normal"; }
    }

    private int starCount;

    private int currentLevelIndex;

    private GameObject levelGo;

    private GameObject mapGo;

    public override void Init()
    {
        UIManager.OpenPanel("GameView", true);
        if (PlayerInfo.CurrentPlayer == null)
        {
            PlayerInfo.DebugPlayerInfo();
        }
        currentLevelIndex = PlayerInfo.CurrentPlayer.SelectedLevelIndex;
    }

    public override void StartGame()
    {
        base.StartGame();
		Time.timeScale = 1.0f;
		UIManager.DispatchMsg("ResetGame");

        starCount = 0;
        LoadLevel();
        StartCoroutine(GameLoop());
    }

    public override void RestartGame()
    {
        ClearLevel();
        StartGame();
    }

    public override void GameOver()
    {
        base.GameOver();
        Time.timeScale = 1.0f;
        ClearLevel ();
    }

	public override void GameResult ()
	{
        base.GameResult();
        Time.timeScale = 1.0f;
        UIManager.OpenPanel ("ResultView",false,starCount,currentLevelIndex);
	}

    public override void PauseGame()
    {
        base.PauseGame();
        Time.timeScale = 0.0f;
    }

    public override void ResumeGame()
    {
        base.ResumeGame();
        Time.timeScale = 1.0f;
    }

    public void GetStar()
    {
        starCount++;
        UIManager.DispatchMsg("GetStar",starCount);
    }

    public void NextLevel()
    {
        currentLevelIndex++;
    }

    private void LoadLevel()
    {
        int package = (currentLevelIndex - 1) / 16 + 1;

        if (package > 2)
        {
            package = 2;
        }
        Debug.Log(currentLevelIndex);

        GameObject levelObj = Resources.Load<GameObject>("level/Level" + currentLevelIndex);
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
			Bomb[] bombs = GameObject.FindObjectsOfType<Bomb>();
			if (bombs == null || bombs.Length == 0)
			{
				GameRunning = false;
                PlayerInfo.CurrentPlayer.PassLevel(currentLevelIndex, starCount);
				StartCoroutine(GameWinDelay(1F));
            }
			yield return new WaitForEndOfFrame ();
		}
	}

	private IEnumerator GameWinDelay(float s){
		yield return new WaitForSeconds(s);
        UIManager.DispatchMsg("GetResult");
		GameResult ();
	}
}
