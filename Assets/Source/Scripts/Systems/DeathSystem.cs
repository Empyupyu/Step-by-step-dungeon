using Supyrb;

public class DeathSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<OnUnitDeathSignal>().AddListener(RemoveUnitAtTurnList);
        Signals.Get<OnPlayerDeathSignal>().AddListener(GameIsOver);
    }

    private void RemoveUnitAtTurnList(Unit unit)
    {
        game.Units.Remove(unit);

        if (!WinCondition()) return;

        Signals.Get<OpenGameOverWindowSignal>().Dispatch();
    }

    private bool WinCondition()
    {
        return game.Units.Count == 0 || game.Level.DungeonGuardian != null && game.Level.DungeonGuardian.IsDeath;
    }

    private void GameIsOver()
    {
        game.GameIsOver = true;
    }
}