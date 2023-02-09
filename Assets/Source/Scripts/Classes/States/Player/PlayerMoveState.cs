using DG.Tweening;
using Supyrb;
using UnityEngine;

public class PlayerMoveState : IState<PlayerStates>
{
    private Unit player;
    private IMovable movable;
    private OnPlayerTurnIsCompletedSignal onPlayerTurnIsCompletedSignal;
    private Vector3 movePoint;

    public PlayerMoveState(StateMachine<PlayerStates> stateMachine, PlayerStates type, Unit player, IMovable movable) : base(stateMachine, type)
    {
        this.player = player;
        this.movable = movable;

        onPlayerTurnIsCompletedSignal = Signals.Get<OnPlayerTurnIsCompletedSignal>();
    }

    public override void OnEnter()
    {
        Move();
    }

    private void Move()
    {
        UpdateMovePoint();
        Animation();

        player.transform.DOMove(movePoint, 2.15f).OnComplete(() =>
        {
            ActionOnEndMoving();
            ChangeCurrentNode();

            stateMachine.SetState(PlayerStates.Idle);
            onPlayerTurnIsCompletedSignal.Dispatch();
        });
    }

    private void UpdateMovePoint()
    {
        var moveDirection = movable.TargetNode.transform.position;
        movePoint = new Vector3(moveDirection.x, player.transform.position.y, moveDirection.z);
    }

    private void Animation()
    {
        player.transform.DOLookAt(movePoint, .5f);
        player.Animator.SetTrigger("Move");
    }

    private void ActionOnEndMoving()
    {
        if (movable.TargetNode.Interactble != null)
        {
            movable.TargetNode.Interactble.Interact();
        }
        else
        {
            Signals.Get<InfoSignal>().Dispatch(player.Name + " Сделал шаг");
        }
    }

    private void ChangeCurrentNode()
    {
        movable.CurrentNode.SetUnit(null);
        movable.SetCurrentNode(movable.TargetNode);
        movable.CurrentNode.SetUnit(player);
        movable.SetTarget(null);
    }

    public override void OnExit()
    {
        player.Animator.SetTrigger("Idle");
    }

    public override void Tick() { }
}