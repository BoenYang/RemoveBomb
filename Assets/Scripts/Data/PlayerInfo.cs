using System;
using System.Collections.Generic;
using GooglePlayGames.OurUtils;
using UnityEngine;

public class PlayerInfo
{
    public int CurrentLevelIndex;

    public int[] LevelStars;

    public static PlayerInfo CurrentPlayer;

    public int SelectedLevelIndex;

    public bool isDebug = false;

    public static void ReadPlayerInfo()
    {
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.CurrentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 1);
        string starStr = PlayerPrefs.GetString("LevelStars", "");
        playerInfo.LevelStars = new int[32];
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
        playerInfo.isDebug = false;
        CurrentPlayer = playerInfo;
    }

    public static void DebugPlayerInfo()
    {
        PlayerInfo playerInfo = new PlayerInfo();
        playerInfo.CurrentLevelIndex = 32;
        playerInfo.LevelStars = new int[32];
        Array.Clear(playerInfo.LevelStars, 0, playerInfo.LevelStars.Length);

        playerInfo.SelectedLevelIndex = 1;
        playerInfo.isDebug = true;
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
        CheckAchievement();
    }

    private void SavePlayerInfo()
    {
        if (isDebug)
        {
            return;
        }

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

    private void CheckAchievement()
    {
        int totalStar = 0;
        for (int i = 0; i < LevelStars.Length; i++)
        {
            totalStar += LevelStars[i];
        }

        if (PlatformUtils.Supported)
        {
            switch (totalStar)
            {

                case 3:
                    GooglePlayTools.IncrementAchievement(GPGSIds.achievement_first_attpact_get_3_stars, totalStar);
                    break;
                case 15:
                    GooglePlayTools.IncrementAchievement(GPGSIds.achievement_get_15_stars, totalStar);
                    break;
                case 48:
                    GooglePlayTools.IncrementAchievement(GPGSIds.achievement_get_48_stars, totalStar);
                    break;
                case 60:
                    GooglePlayTools.IncrementAchievement(GPGSIds.achievement_become_more_proficient_get_60_stars, totalStar);
                    break;
                case 96:
                    GooglePlayTools.IncrementAchievement(GPGSIds.achievement_reach_the_limit_get_96_stars, totalStar);
                    break;
            }
        }
    }
}
