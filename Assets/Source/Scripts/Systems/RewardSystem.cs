using Supyrb;

public class RewardSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<RewardSignal>().AddListener(Reward);
    }

    private void Reward()
    {
        Signals.Get<InfoSignal>().Dispatch(game.Player.Name + " �������� ������� �� ����������� ����������!");
        Signals.Get<PlayerOpenTeleportWindowSignal>().Dispatch(game.Level.Portals[game.Level.Portals.Count - 1]);
    }
}