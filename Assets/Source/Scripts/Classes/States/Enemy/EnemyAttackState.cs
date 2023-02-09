using UnityEngine;

public class EnemyAttackState : IState<EnemiesStates>
{
    public EnemyAttackState(StateMachine<EnemiesStates> stateMachine, EnemiesStates type) : base(stateMachine, type)
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