public class EnemyDeadState : IState<EnemiesStates>
{
    public EnemyDeadState(StateMachine<EnemiesStates> stateMachine, EnemiesStates type) : base(stateMachine, type)
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