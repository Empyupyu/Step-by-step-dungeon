public class EnemyDeadState : IState<EnemiesStates>
{
    private Enemy enemy;

    public EnemyDeadState(StateMachine<EnemiesStates> stateMachine, EnemiesStates type, Enemy enemy) : base(stateMachine, type)
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        enemy.Animator.SetTrigger("Death");
    }

    public override void OnExit()
    {
    }

    public override void Tick()
    {
    }
}