﻿using System.Collections;
using UnityEngine;

public class NormalMode : GameModeBase
{
    public override string Mode
    {
        get { return "Normal"; }
    }
    public int StarCount;

    private int LevelIndex = 1;

    private GameObject levelGo;

    private GameObject mapGo;

    public override void Init()
    {
        UIManager.OpenPanel("GameView", true);
    }

    public override void StartGame()
    {
        base.StartGame();
        LoadLevel();
        StarCount = 0;
        Time.timeScale = 1.0f;
        UIManager.DispatchMsg("ResetGame");
        StartCoroutine(GameLoop());
    }

    public override void RestartGame()
    {
        ClearLevel();
        StartGame();
    }

    public override void GameOver()
    {
        GameRunning = false;
        Time.timeScale = 1.0f;
        ClearLevel ();
    }

	public override void GameResult ()
	{
        GameRunning = false;
        Time.timeScale = 1.0f;
        UIManager.OpenPanel ("ResultView");
	}

    public override void PauseGame()
    {
        GamePaused = true;
        Time.timeScale = 0.0f;
    }

    public override void ResumeGame()
    {
        GamePaused = false;
        Time.timeScale = 1.0f;
    }

    public void GetStar()
    {
        StarCount++;
        UIManager.DispatchMsg("GetStar");
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
