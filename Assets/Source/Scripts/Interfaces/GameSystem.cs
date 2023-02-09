using UnityEngine;

public abstract class GameSystem : MonoBehaviour, IGameSystem
{
    protected ConfigData congfig;
    protected GameData game;
    protected PlayerData player;

    public void InitializeData(ConfigData config, GameData game, PlayerData player)
    {
        this.congfig = config;
        this.game = game;
        this.player = player;
    }

    public virtual void OnAwake() { }

    public virtual void OnStart() { }

    public virtual void OnUpdate() { }
}