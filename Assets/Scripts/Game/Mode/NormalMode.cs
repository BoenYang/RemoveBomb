
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
        GameRunning = true;
        LoadLevel();
    }

    public override void PauseGame()
    {
        GameRunning = false;
        Time.timeScale = 0;
    }

    public override void ResumeGame()
    {
        GameRunning = true;
        Time.timeScale = 1.0f;
    }

    public override void RestartGame()
    {
        Time.timeScale = 1.0f;
        ClearLevel();
        StartGame();
    }

    public override void GameOver()
    {
        Time.timeScale = 1.0f;
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
}
