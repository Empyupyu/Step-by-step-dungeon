using DG.Tweening;
using Supyrb;
using UnityEngine;

public class PlayerMoveState : IState<PlayerStates>
{
    private Unit player;
    private IMovable movable;
    private OnPlayerTurnIsCompletedSignal onPlayerTurnIsCompletedSignal;

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
        var moveDirection = movable.TargetNode.transform.position;
        var movePoint = new Vector3(moveDirection.x, player.transform.position.y, moveDirection.z);

        player.transform.DOLookAt(movePoint, .5f);

        player.Animator.SetTrigger("Move");

        player.transform.DOMove(movePoint, 2.15f).OnComplete(() =>
        {
            if (movable.TargetNode.Interactble != null)
            {
                movable.TargetNode.Interactble.Interact();
            }
            else
            {
                Signals.Get<InfoSignal>().Dispatch(player.Name + " Сделал шаг");
            }

            stateMachine.SetState(PlayerStates.Idle);
            onPlayerTurnIsCompletedSignal.Dispatch();
        });
    }

    public override void OnExit()
    {
        player.Animator.SetTrigger("Idle");
    }

    public override void Tick()
    {
        
    }
}