using System;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

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
}
