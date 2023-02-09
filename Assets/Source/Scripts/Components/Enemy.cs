using System.Collections.Generic;
using Supyrb;
using UnityEngine;

public class Enemy : StateMachineObject<EnemiesStates>, IMovable, IDamageble, IAttackable, IFinderTargetUnit
{
    [field : SerializeField] public int Health { get; private set; }
    [field : SerializeField] public int Damage { get; private set; }
    public Node TargetNode { get; private set; }
    public Node CurrentNode { get; private set; }
    public float MoveTimeToTargetNode { get; private set; }
    public float TimeToLookAt { get; private set; }
    public float Radius { get; private set; }

    private Player target;
    private List<Node> neighborNodes = new List<Node>();

    public void SetHealth(int value)
    {
        Health = value;
    }

    public void SetMoveTimeToTargetNode(float time)
    {
        MoveTimeToTargetNode = time;
    }

    public void SetTimeToLookAt(float time)
    {
        TimeToLookAt = time;
    }

    public void SetDamage(int damage)
    {
        Damage = damage;
    }

    public void SetRadius(float radius)
    {
        Radius = radius;
    }

    public void FindTargetInRadius()
    {
        if (target != null) return;

        var units = Physics.OverlapSphere(transform.position, Radius);

        for (int i = 0; i < units.Length; i++)
        {
            if(units[i].TryGetComponent<Player>(out var player))
            {
                target = player;

                return;
            }
        }
    }

    public void SetCurrentNode(Node node)
    {
        CurrentNode = node;
    }

    public void ApplyDamage(DamageInfoHolder damage)
    {
        Signals.Get<ApplyDamageSingal>().Dispatch(damage);
    }

    public override void DoAction()
    {
        FindTargetInRadius();

        if(target != null )
        {

        }


        Debug.Log(nameof(Enemy) + " do somthing");
    }

    private void GetNeighborNodes()
    {
       
        
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