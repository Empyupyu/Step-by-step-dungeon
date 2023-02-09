using Supyrb;

public class Player : StateMachineObject<PlayerStates>, IMovable, IDamageble, IAttackable
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

    public void SetTarget(Node node)
    {
        TargetNode = node;
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
}