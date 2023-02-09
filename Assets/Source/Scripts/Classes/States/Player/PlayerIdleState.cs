public class PlayerIdleState : IState<PlayerStates>
{
    public PlayerIdleState(StateMachine<PlayerStates> stateMachine, PlayerStates type) : base(stateMachine, type)
    {
    }

    public override void OnEnter()
    {

    }

    public override void OnExit()
    {
       
    }

    public override void Tick()
    {
        
    }
}