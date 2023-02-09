using System.Collections.Generic;
using Supyrb;

public class Player : StateMachineObject<PlayerStates>, IMovable, IDamageble, IAttackable
{
    public Node TargetNode { get; private set; }
    public Node CurrentNode { get; private set; }
    public int Health { get; private set; }
    public int Damage { get; private set; }
    public float MoveTimeToTargetNode { get; private set; }
    public float TimeToLookAt { get; private set; }
    public float AvailableRadius { get; private set; }
    public List<Node> AvailableNodes { get; private set; } = new List<Node>();

    #region Setters

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

    public void SetAvailableRadius(float radius)
    {
        AvailableRadius = radius;
    }

    public void SetDamage(int damage)
    {
        Damage = damage;
    }

    public void SetTarget(Node node)
    {
        TargetNode = node;
    }

    public void SetCurrentNode(Node node)
    {
        CurrentNode = node;
    }

    public void SetAvailableNodes(List<Node> availableNodes)
    {
        AvailableNodes = availableNodes;
    }

    #endregion Setters

    public void ApplyDamage(DamageInfoHolder damage)
    {
        Signals.Get<ApplyDamageSingal>().Dispatch(damage);
    }

    public void ClearAvailableNodes()
    {
        AvailableNodes.Clear();
    }

    public override void DoAction()
    {
        if(TargetNode.Unit == null)
        {
            StateMachine.SetState(PlayerStates.Move);
        }
        else  if(TargetNode.Unit != null)
        {
            StateMachine.SetState(PlayerStates.Attack);
        }
    }

    #region StateMachine

    protected override void GetComponents() { }
    protected override void Init() { }

    protected override void SetInitialState()
    {
        StateMachine.SetState(PlayerStates.Idle);
    }

    protected override void LoadStates()
    {
        StateMachine.AddState(new PlayerIdleState(StateMachine, PlayerStates.Idle));
        StateMachine.AddState(new PlayerMoveState(StateMachine, PlayerStates.Move, this, this));
        StateMachine.AddState(new PlayerAttackState(StateMachine, PlayerStates.Attack, this, this));
    }

    #endregion StateMachine
}