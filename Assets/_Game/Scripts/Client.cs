using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Client : MonoBehaviour {

    public static Client Ins;

    public PlayerInfo Player = new PlayerInfo();

    public int TotalLevel = 32;

    public void Awake() {
        Ins = this;
        ReadPlayerInfo();
    }

    private void ReadPlayerInfo()
    {
        Player.PassedLevelNumber = PlayerPrefs.GetInt("PassedLevelNumber", 1);
        string levelStarStr = PlayerPrefs.GetString("LevelStarNum", "");
        if (levelStarStr.Equals(""))
        {
            Player.LevelStars = new int[TotalLevel];
        }
        else if (levelStarStr.Length == TotalLevel)
        {
            Player.LevelStars = new int[TotalLevel];
            for (int i = 0; i < TotalLevel; i++)
            {
                Player.LevelStars[i] = int.Parse(levelStarStr[i]+"");
            }
        }
        else
        {
            Debug.LogError("User info error");
        }

        Player.StarNum = PlayerPrefs.GetInt("StarNum", 0);
    }

    public void SavePlayerInfo()
    {
        PlayerPrefs.SetInt("PassedLevelNumber", Player.PassedLevelNumber);

        string str = "";
        foreach (var n in Player.LevelStars)
        {
            str += n;
        }

        PlayerPrefs.SetString("LevelStarNum", str);
        PlayerPrefs.SetInt("StarNum", Player.StarNum);
        Debug.Log("save" + str);
    }

    private void Start()
    {
        UIRootController.Ins.ShowUI(UIType.MainUI);
    }
}


