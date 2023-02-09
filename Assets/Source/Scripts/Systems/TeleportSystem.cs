using Supyrb;

public class TeleportSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<TeleportationOnNextLevelSignal>().AddListener(Teleportation);
        Signals.Get<PlayerOpenTeleportWindowSignal>().AddListener(TeleportInfo);
    }

    private void TeleportInfo(Portal teleport)
    {
        Signals.Get<InfoSignal>().Dispatch($"{game.Player.Name} прикоснулся к {nameof(teleport)}...");
    }

    private void Teleportation(Portal teleport)
    {
        player.LevelIndex = teleport.TransitionOnLevelIndex;
    }
}