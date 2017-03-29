using System;
using System.Collections;
using UnityEngine;

public abstract class GameModeBase
{

    public bool GameRunning = false;

    public abstract string Mode { get; }

    public virtual void Init()
    {

    }

    public virtual void OnEnterScene()
    {

    }

    public virtual void StartGame()
    {
    }

    public virtual IEnumerator GameLoop()
    {
        yield return 0;
    }

    public virtual void GameOver()
    {

    }

    public virtual void GameResult()
    {

    }

    public virtual void PauseGame()
    {

    }

    public virtual void ResumeGame()
    {

    }

    public virtual void RestartGame()
    {

    }

    public static GameModeBase CreateGameMode(string mode)
    {
        string typeName = mode + "Mode";
        Type modeType = Type.GetType(typeName);
        if (modeType == null)
        {
            Debug.LogError("创建游戏模式失败,找不到类名为" + typeName + "的类");
            return null;
        }
        return (GameModeBase)Activator.CreateInstance(modeType);
    }

}

