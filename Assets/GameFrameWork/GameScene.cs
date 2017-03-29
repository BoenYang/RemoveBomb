using UnityEngine;

public class GameScene : MonoBehaviour
{
    public string GameMode = "Normal";

    public GameModeBase Game;

    public static GameScene Instance;

    void Awake()
    {
        Instance = this;
        Game = GameModeBase.CreateGameMode(GameMode);
        Game.Init();
        Game.OnEnterScene();
    }

    void Start()
    {
        Game.StartGame();
    }

}
