using System.Collections.Generic;
using Supyrb;
using UnityEngine;

public class Enemy : StateMachineObject<EnemiesStates>, IMovable, IDamageble, IAttackable, IFinderTargetUnit
{
    [field : SerializeField] public int Health { get; private set; }
    [field : SerializeField] public int Damage { get; private set; }
    [field : SerializeField] public float AvailableRadius { get; private set; }
    [field : SerializeField] public float FindTargetUnitRadius { get; private set; }
    public Node TargetNode { get; private set; }
    public Node CurrentNode { get; private set; }
    public float MoveTimeToTargetNode { get; private set; }
    public float TimeToLookAt { get; private set; }
    public List<Node> AvailableNodes { get; private set; } = new List<Node>();
    public Player TargetUnit { get; private set; }
    public bool IsDeath { get; private set; }

    private List<Node> neighborNodes = new List<Node>();

    #region Setters

    public void SetHealth(int value)
    {
        Health = value;

        if(Health == 0 && !IsDeath)
        {
            IsDeath = true;

            Signals.Get<OnUnitDeathSignal>().Dispatch(this);

            StateMachine.SetState(EnemiesStates.Death);
        }
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

    public void SetRadius(float radius)
    {
        FindTargetUnitRadius = radius;
    }

    public void SetCurrentNode(Node node)
    {
        CurrentNode = node;
    }

    public void SetTarget(Node node)
    {
        TargetNode = node;
    }

    public void SetAvailableNodes(List<Node> availableNodes)
    {
        AvailableNodes = availableNodes;
    }

    #endregion Setters

    public void FindTargeUnittInSphere()
    {
        if (TargetUnit != null) return;

        var units = Physics.OverlapSphere(transform.position, FindTargetUnitRadius);

        for (int i = 0; i < units.Length; i++)
        {
            if (units[i].TryGetComponent<Player>(out var player))
            {
                Ray ray = new Ray(transform.position + new Vector3(0, 0.5f, 0), player.transform.position + new Vector3(0, 0.5f, 0) - transform.position + new Vector3(0, 0.5f, 0));

                Debug.DrawRay(transform.position + new Vector3(0,0.5f,0), player.transform.position + new Vector3(0, 0.5f, 0) - transform.position + new Vector3(0, 0.5f, 0), Color.red, 3);

                if (Physics.Raycast(ray, out var hit))
                {
                    if (hit.collider.GetComponent<Player>() == null) return;
                }

                TargetUnit = player;

                return;
            }
        }
    }

    public void ApplyDamage(DamageInfoHolder damage)
    {
        Signals.Get<ApplyDamageSingal>().Dispatch(damage);
    }

    public override void DoAction()
    {
        FindTargeUnittInSphere();

        if(TargetUnit != null)
        {
            GetNeighborNodes();

            var node = FindUnitTargetOnNeighborNodes();

            if (node != null)
            {
                SetTarget(node);

                StateMachine.SetState(EnemiesStates.Attack);
            }
            else
            {
                FindNearestNodeOnDirectionToTargetUnit();

                StateMachine.SetState(EnemiesStates.Move);
            }
        }
        else
        {
            Debug.Log(nameof(Enemy) + " do somthing");
        }
    }

    private void GetNeighborNodes()
    {
        Signals.Get<FindNeighborNodesSignal>().Dispatch(this);
    }

    private Node FindUnitTargetOnNeighborNodes()
    {
        for (int i = 0; i < AvailableNodes.Count; i++)
        {
            if(AvailableNodes[i].Unit == TargetUnit)
                return AvailableNodes[i];
        }

        return null;
    }

    private void FindNearestNodeOnDirectionToTargetUnit()
    {
        Signals.Get<EnemyPathfindingSignal>().Dispatch(this);
    }

    public void ClearAvailableNodes()
    {
        AvailableNodes.Clear();
    }

    #region StateMachine

    protected override void GetComponents() { }

    protected override void Init() { }

    protected override void SetInitialState()
    {
        StateMachine.SetState(EnemiesStates.Idle);
    }

    protected override void LoadStates()
    {
        StateMachine.AddState(new EnemyIdleState(StateMachine, EnemiesStates.Idle));
        StateMachine.AddState(new EnemyMoveState(StateMachine, EnemiesStates.Move, this));
        StateMachine.AddState(new EnemyAttackState(StateMachine, EnemiesStates.Attack, this));
        StateMachine.AddState(new EnemyDeadState(StateMachine, EnemiesStates.Death, this));
    }

    #endregion StateMachine
}