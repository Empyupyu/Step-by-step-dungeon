using UnityEngine;

public class LevelSpawnerSystem : GameSystem
{
    public override void OnAwake()
    {
        CreateLevel();
    }

    private void CreateLevel()
    {
        var level = Resources.Load<Level>($"Levels/Level {congfig.StartLevelIndex + 1}");
        game.Level = Instantiate(level);
    }
}