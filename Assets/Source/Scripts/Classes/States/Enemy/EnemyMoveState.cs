public class EnemyMoveState : IState<EnemiesStates>
{
    public EnemyMoveState(StateMachine<EnemiesStates> stateMachine, EnemiesStates type) : base(stateMachine, type)
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