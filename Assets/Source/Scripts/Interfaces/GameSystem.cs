using UnityEngine;

public abstract class GameSystem : MonoBehaviour, IGameSystem
{
    protected ConfigData congfig;
    protected GameData game;

    public void InitializeData(ConfigData config, GameData game)
    {
        this.congfig = config;
        this.game = game;
    }

    public virtual void OnAwake() { }

    public void OnStart() { }

    public void OnUpdate() { }
}