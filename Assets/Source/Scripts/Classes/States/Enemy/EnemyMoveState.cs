using DG.Tweening;
using Supyrb;
using UnityEngine;

public class EnemyMoveState : IState<EnemiesStates>
{
    private Enemy enemy;
    private Vector3 movePoint;

    public EnemyMoveState(StateMachine<EnemiesStates> stateMachine, EnemiesStates type, Enemy enemy) : base(stateMachine, type)
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        Move();
    }

    private void Move()
    {
        UpdateMovePoint();
        Animation();

        enemy.transform.DOMove(movePoint, enemy.MoveTimeToTargetNode).OnComplete(() =>
        {
            Signals.Get<InfoSignal>().Dispatch(enemy.Name + " »дет к цели");

            ChangeCurrentNode();

            stateMachine.SetState(EnemiesStates.Idle);
        });
    }

    private void UpdateMovePoint()
    {
        var moveDirection = enemy.TargetNode.transform.position;
        movePoint = new Vector3(moveDirection.x, enemy.transform.position.y, moveDirection.z);
    }

    private void Animation()
    {
        enemy.transform.DOLookAt(movePoint, enemy.TimeToLookAt);
        enemy.Animator.SetTrigger("Move");
    }

    private void ChangeCurrentNode()
    {
        enemy.CurrentNode.SetUnit(null);
        enemy.SetCurrentNode(enemy.TargetNode);
        enemy.CurrentNode.SetUnit(enemy);
        enemy.SetTarget(null);
    }

    public override void OnExit()
    {
        enemy.Animator.SetTrigger("Idle");
    }

    public override void Tick() { }
}