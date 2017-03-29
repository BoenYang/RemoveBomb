using System;
using System.Collections;
using UnityEngine;

public abstract class GameModeBase
{
    public bool GameRunning = false;

	public bool GamePaused = false;

    public abstract string Mode { get; }

    public virtual void Init()
    {
		GameRunning = false;
		GamePaused = false;
	}

    public virtual void OnEnterScene()
    {

    }

    public virtual void StartGame()
    {
		GameRunning = true;
		GamePaused = false;
		Time.timeScale = 1.0f;
		StartCoroutine (GameLoop ());
    }

	protected virtual IEnumerator GameLoop()
    {
        yield return 0;
    }

    public virtual void GameOver()
    {
		GameRunning = false;
		Time.timeScale = 1.0f;
    }

    public virtual void GameResult()
    {
		GameRunning = false;
		Time.timeScale = 1.0f;
    }

    public virtual void PauseGame()
    {
		GamePaused = true;
		Time.timeScale = 0.0f;
    }

    public virtual void ResumeGame()
    {
		GamePaused = false;
		Time.timeScale = 1.0f;
    }

    public virtual void RestartGame()
    {
		
    }

	protected void StartCoroutine(IEnumerator coroutine){
		GameScene.Instance.StartCoroutine (coroutine);
	}

    public static GameModeBase CreateGameMode(string mode)
    {
        string typeName = mode + "Mode";
        Type modeType = Type.GetType(typeName);
        if (modeType == null)
        {
            Debug.LogError("找不到类名为" + typeName + "的类");
            return null;
        }
        return (GameModeBase)Activator.CreateInstance(modeType);
    }


}

