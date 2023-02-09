using System.Collections;
using System.Linq;
using DG.Tweening;
using Supyrb;
using UnityEngine;

public class PlayerAttackState : IState<PlayerStates>
{
    private Player player;
    private IAttackable attackable;
    private OnPlayerTurnIsCompletedSignal onPlayerTurnIsCompletedSignal;

    public PlayerAttackState(StateMachine<PlayerStates> stateMachine, PlayerStates type, Player player, IAttackable attackable) : base(stateMachine, type)
    {
        this.player = player;
        this.attackable = attackable;

        onPlayerTurnIsCompletedSignal = Signals.Get<OnPlayerTurnIsCompletedSignal>();
    }

    public override void OnEnter()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        var movePoint = new Vector3(player.TargetNode.Unit.transform.position.x, player.transform.position.y, player.TargetNode.Unit.transform.position.z);

        player.transform.DOLookAt(movePoint, .5f).OnComplete(() => player.StartCoroutine(DelayTransitionToOtherState()));
    }

    private IEnumerator DelayTransitionToOtherState()
    {
        player.Animator.SetTrigger("Attack");

        var clip = player.Animator.runtimeAnimatorController.animationClips.FirstOrDefault(a => a.name.Equals("Attack"));

        yield return new WaitForSeconds(clip.length / 2f);

        player.TargetNode.Unit.TryGetComponent<IDamageble>(out var damageble);
        damageble.ApplyDamage(new DamageInfoHolder(player.Name, player.TargetNode.Unit.Name, player, damageble, attackable.Damage));

        yield return new WaitForSeconds(1f);

        stateMachine.SetState(PlayerStates.Idle);
    }

    public override void OnExit()
    {
        onPlayerTurnIsCompletedSignal.Dispatch();
    }

    public override void Tick()
    {
        
    }
}