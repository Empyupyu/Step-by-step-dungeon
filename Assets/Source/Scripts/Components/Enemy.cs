using Supyrb;
using UnityEngine;

public class Enemy : StateMachineObject<EnemiesStates>, IMovable, IDamageble, IAttackable
{
    public Node TargetNode { get; private set; }
    public int Health { get; private set; }
    public int Damage { get; private set; }

    public void SetHealth(int value)
    {
        Health = value;
    }

    public void ApplyDamage(DamageInfoHolder damage)
    {
        Signals.Get<ApplyDamageSingal>().Dispatch(damage);
    }

    public override void DoAction()
    {
        Debug.Log(nameof(Enemy) + " do somthing");
    }

    public void SetTarget(Node node)
    {
        TargetNode = node;
    }

    protected override void GetComponents() { }

    protected override void Init() { }

    protected override void SetInitialState()
    {
        StateMachine.SetState(EnemiesStates.Idle);
    }

    protected override void LoadStates()
    {
        StateMachine.AddState(new EnemyIdleState(StateMachine, EnemiesStates.Idle));
        StateMachine.AddState(new EnemyMoveState(StateMachine, EnemiesStates.Move));
        StateMachine.AddState(new EnemyAttackState(StateMachine, EnemiesStates.Attack));
        StateMachine.AddState(new EnemyDeadState(StateMachine, EnemiesStates.Death));
    }

}