
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

        int package = LevelIndex / 16 + 1;

        GameObject levelObj = Resources.Load<GameObject>("level/Level" + LevelIndex);
        levelGo = GameObject.Instantiate(levelObj);
        levelGo.transform.localPosition = Vector3.zero;
        levelGo.transform.localScale = Vector3.one;

        GameObject mapObj = Resources.Load<GameObject>("Map/Map" + package);
        mapGo = GameObject.Instantiate(mapObj);
        mapGo.transform.localPosition = Vector3.zero;
        mapGo.transform.localScale = Vector3.one;
    }

}
