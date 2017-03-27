using System;
using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager Ins;

    public int Level;

    public bool GamePaused = false;

    public bool GameStarted = false;

    private GameObject levelGo;

    public int GotStar;

    public Action<int> OnGotStar;

    public Action OnGameReset;

    void Awake() {
        Ins = this;
    }

    private void Update()
    {
        if (GameStarted)
        {
            GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
            if (bombs == null || bombs.Length == 0)
            {
                GameStarted = false;
                StartCoroutine(GameWinDelay(1F));
            }
        }
    }

    public void GameStart() {
        GamePaused = false;
        Invoke("LoadLevel", 0.5f);
    }

    public void LoadLevel()
    {
        ClearLevel();
        GameObject levelObj = Resources.Load<GameObject>("level/Level" + Level);
        levelGo = Instantiate(levelObj);
        GameStarted = true;
        if (OnGameReset != null)
        {
            OnGameReset();
        }
    }

    public IEnumerator GameWinDelay(float s)
    {
        yield return new WaitForSeconds(s);
        GameWin();
    }

    public void GameWin() {
        UIRootController.Ins.ShowUI(UIType.ResultUI,false,false);
//        if (Client.Ins.Player.PassedLevelNumber <= Level)
//        {
//            Client.Ins.Player.PassedLevelNumber++;
//        }
//        if (GotStar > Client.Ins.Player.LevelStars[Level - 1])
//        {
//            Client.Ins.Player.LevelStars[Level - 1] = GotStar;
//        }
//        Client.Ins.SavePlayerInfo();
    }

    public void ClearLevel()
    {
        if (levelGo != null)
        {
            Destroy(levelGo);
        }
        GotStar = 0;
    }

    public void GamePause()
    {
        Time.timeScale = 0;
        GamePaused = true;
    }

    public void GameReset() {
        GamePaused = false;
        GameStarted = false;
        Invoke("LoadLevel", 0.5f);
    }

    public void GameEnd()
    {
        Time.timeScale = 1.0f;
        GameStarted = false;
        GamePaused = true;
        ClearLevel();
    }

    public void GameResume()
    {
        Time.timeScale = 1.0f;
        GamePaused = false;
    }

    public void AddStar()
    {
        GotStar ++;
        if (OnGotStar != null)
        {
            OnGotStar(GotStar);
        }
    }

    public void NextLevel()
    {
        Level++;
        GameReset();
    }
}
