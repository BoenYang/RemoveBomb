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


    #region 保存游戏数据

    private static Action<byte[]> openGameBytesDataCallback;

    private static ISavedGameMetadata currentOpenedGameData;

    public static void OpenSavedGameUI(string uiTittle,int maxDisplayGame,bool showCreateUI,bool showDeleteUI,Action<byte[]> selectedCallBack)
    {
        openGameBytesDataCallback = selectedCallBack;
        ((PlayGamesPlatform)Social.Active).SavedGame.ShowSelectSavedGameUI(uiTittle, 4, false, false, OnSelectedGame);
    }

    private static void OnSelectedGame(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            Debug.Log("Success Select game: " + status);
            string fileName = game.Filename;
            OpendSavedGame(fileName, DataSource.ReadCacheOrNetwork,ConflictResolutionStrategy.UseLongestPlaytime, openGameBytesDataCallback);
        }
        else
        {
            Debug.Log("Error Select game: " + status);
        }
    }


    public static void OpendSavedGame(string fileName,DataSource dataSource,ConflictResolutionStrategy strategy, Action<byte[]> gameOpenCallback)
    {
        openGameBytesDataCallback = gameOpenCallback;
        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution(fileName, dataSource, strategy, OnOpenSavedGame);
    }

    private static void OnOpenSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        currentOpenedGameData = game;
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Success Opening game: " + status);
            ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, OnReadGameDataCallBack);
        }
        else
        {
            Debug.Log("Error Opening game: " + status);
        }
    }

    private static void OnReadGameDataCallBack(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Success Reading game: " + status);
            if (openGameBytesDataCallback != null)
            {
                openGameBytesDataCallback.Invoke(data);
            }
        }
        else
        {
            Debug.Log("Error reading game: " + status);
        }
    }


    public static void WriteGameData(byte[] data)
    {
        SavedGameMetadataUpdate.Builder builder = new
        SavedGameMetadataUpdate.Builder().WithUpdatedDescription("Saved Game at " + DateTime.Now);
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(currentOpenedGameData, updatedMetadata, data, SavedGameWritten);
    }

    private static void SavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("Game " + game.Description + " written");
        }
        else
        {
            Debug.LogWarning("Error saving game: " + status);
        }
    }
    
    #endregion
}
