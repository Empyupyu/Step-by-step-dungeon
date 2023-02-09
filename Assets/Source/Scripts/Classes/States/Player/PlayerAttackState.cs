using Supyrb;

public class PlayerAttackState : IState<PlayerStates>
{
    private Unit player;
    private OnPlayerTurnIsCompletedSignal onPlayerTurnIsCompletedSignal;

    public PlayerAttackState(StateMachine<PlayerStates> stateMachine, PlayerStates type, Unit player) : base(stateMachine, type)
    {
        this.player = player;

        onPlayerTurnIsCompletedSignal = Signals.Get<OnPlayerTurnIsCompletedSignal>();
    }

    public override void OnEnter()
    {
        player.Animator.SetTrigger("Attack");
    }

    public override void OnExit()
    {
        onPlayerTurnIsCompletedSignal.Dispatch();
    }

    public override void Tick()
    {
        
    }
}