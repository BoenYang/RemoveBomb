using UnityEngine;
using System.Collections;
//using GooglePlayGames;
//using GooglePlayGames.BasicApi;
//using GooglePlayGames.BasicApi.Multiplayer;

public class GooglePlayTest : MonoBehaviour
{

    private bool signSuccess = false;

    private bool signing = false;

    private bool signEnd = false;

    string signState = "signing +++++++++++++";

    void Awake()
    {
        signSuccess = false;
        signing = false;
        signEnd = false;
    }

    public void Init()
    {
        /*PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
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
        PlayGamesPlatform.Activate();*/
    }


    public void Sign()
    {
        signing = true;
        Social.localUser.Authenticate((bool success) =>
        {
            signSuccess = success;
            signEnd = true;
        });
    }

    void OnGUI()
    {
        if (GUILayout.Button("Init", GUILayout.Width(200),GUILayout.Height(50)))
        {
            Init();
        }

        GUILayout.Space(20);
        if (GUILayout.Button("Sign", GUILayout.Width(200), GUILayout.Height(50)))
        {
            Sign();
        }

        if (signing)
        {
            signState = "signing ----------------";
        }


        if (signEnd)
        {
            signState = "sign end +++++++++++++++";
            GUILayout.Label(signState);

            if (signSuccess)
            {
                GUILayout.Label("signing failed");
            }
            else
            {
                GUILayout.Label("signing success");
            }
        }
    
    }

}
