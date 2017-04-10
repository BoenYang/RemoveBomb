using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;

public class GooglePlayTools{

    public static void Init()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
        // enables saving game progress.
        .EnableSavedGames()
        //        // registers a callback to handle game invitations received while the game is not running.
        //        .WithInvitationDelegate((Invitation invitation, bool shouldAutoAccept) =>
        //        {
        //            
        //        })
        //        // registers a callback for turn based match notifications received while the
        //        // game is not running.
        //        .WithMatchDelegate((TurnBasedMatch match, bool shouldAutoLaunch) =>
        //        {
        //            
        //        })
        //        // require access to a player's Google+ social graph (usually not needed)
        //        .RequireGooglePlus()
        .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();
    }

    public static void Sign(Action<bool> callBack)
    {
        Social.localUser.Authenticate(callBack);
    }

    public static void ReportAchieveProcess(string achieve,float process)
    {
        Debug.Log("更新成就" + achieve + "  " + process);
        Social.ReportProgress(achieve,process, (success) =>
        {
            if (success)
            {
                Debug.Log("更新成就进度成功");
            }
            else
            {
                Debug.Log("更新成就进度失败");
            }
        });
    }

    public static void IncrementAchievement(string achieve, int step)
    {
        Debug.Log("上传成就" + achieve + "  " + step);
        PlayGamesPlatform.Instance.IncrementAchievement(achieve,step, (success) =>
        {
            if (success)
            {
                Debug.Log("增加成就进度成功");
            }
            else
            {
                Debug.Log("增加成就进度失败");
            }
        });
    }

    public static void ReportScore(string leaderboard, int score)
    {
        Social.ReportScore(score, leaderboard, (bool success) =>
        {
            if (success)
            {
                Debug.Log("更新排行榜成功");
            }
            else
            {
                Debug.Log("更新排行榜失败");
            }
        });
    }

    public static void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder.WithUpdatedPlayedTime(totalPlaytime).WithUpdatedDescription("Saved game at " + DateTime.Now);
    
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    private static void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("保存游戏数据成功");
        }
        else {
            Debug.Log("保存游戏数据失败");
        }
    }


    public static void ReadSavedGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private static void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("读取游戏数据成功");
        }
        else {
            Debug.Log("读取游戏数据失败");
        }
    }
}
