
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo
{
    public int CurrentLevelIndex;

    public int[] LevelStars;

    public static PlayerInfo CurrentPlayer;

    public int SelectedLevelIndex;

    public static void ReadPlayerInfo()
    {
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        string starStr = PlayerPrefs.GetString("LevelStars", "");
        playerInfo.LevelStars = new int[32];
        Debug.Log(playerInfo.CurrentLevelIndex);
        Array.Clear(playerInfo.LevelStars, 0, playerInfo.LevelStars.Length);
        if (!string.IsNullOrEmpty(starStr))
        {
            Debug.Log(starStr);
            string[] stars = starStr.Split('|');
            for (int i = 0; i < stars.Length; i++)
            {
                int star = int.Parse(stars[i]);
                playerInfo.LevelStars[i] = star;
            }
        }
        CurrentPlayer = playerInfo;
    }

    public void PassLevel(int levelIndex,int star)
    {
        if (LevelStars[levelIndex - 1] < star)
        {
            LevelStars[levelIndex - 1] = star;
        }

        if (levelIndex >= CurrentLevelIndex)
        {
            CurrentLevelIndex = levelIndex;
            CurrentLevelIndex++;
        }
        SavePlayerInfo();
    }

    private void SavePlayerInfo()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex",CurrentLevelIndex);
        string levelStr = "";
        for (int i = 0; i < LevelStars.Length; i++)
        {
            if (i != LevelStars.Length - 1)
            {
                levelStr += LevelStars[i] + "|";
            }
            else
            {
                levelStr += LevelStars[i].ToString();
            }
        }
        PlayerPrefs.SetString("LevelStars",levelStr);
    }

}
