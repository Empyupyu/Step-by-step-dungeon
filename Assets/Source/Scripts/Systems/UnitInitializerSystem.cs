public class UnitInitializerSystem : GameSystem
{
    public override void OnAwake()
    {
        InitializeEnemyUnits();
    }

    private void InitializeEnemyUnits()
    {
        for (int i = 0; i < game.Level.Enemies.Count; i++)
        {
            var unit = game.Level.Enemies[i];
            game.Units.Add(unit);

            if (!congfig.IsDebug) continue;

            unit.SetHealth(congfig.EnemyHealthOnDebugMode);
        }
    }
}