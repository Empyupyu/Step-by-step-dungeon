using UnityEngine;

public class LevelSpawnerSystem : GameSystem
{
    public override void OnAwake()
    {
        player.LevelIndex = congfig.IsDebug ? congfig.StartLevelIndex : player.LevelIndex;

        CreateLevel();
    }

    private void CreateLevel()
    {
        var level = Resources.Load<Level>($"Levels/Level {player.LevelIndex + 1}");
        game.Level = Instantiate(level);
    }
}