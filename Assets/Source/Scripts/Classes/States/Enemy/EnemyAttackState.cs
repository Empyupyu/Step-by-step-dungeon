using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class EnemyAttackState : IState<EnemiesStates>
{
    private Enemy enemy;

    public EnemyAttackState(StateMachine<EnemiesStates> stateMachine, EnemiesStates type, Enemy enemy) : base(stateMachine, type)
    {
        this.enemy = enemy;
    }

    public override void OnEnter()
    {
        LookAtTarget();
    }

    private void LookAtTarget()
    {
        var movePoint = new Vector3(enemy.TargetNode.Unit.transform.position.x, enemy.transform.position.y, enemy.TargetNode.Unit.transform.position.z);

        enemy.transform.DOLookAt(movePoint, .5f).OnComplete(() => enemy.StartCoroutine(DelayTransitionToOtherState()));
    }

    private IEnumerator DelayTransitionToOtherState()
    {
        enemy.Animator.SetTrigger("Attack");

        var clip = enemy.Animator.runtimeAnimatorController.animationClips.FirstOrDefault(a => a.name.Equals("Attack"));

        yield return new WaitForSeconds(clip.length / 2f);

        enemy.TargetNode.Unit.TryGetComponent<IDamageble>(out var damageble);
        damageble.ApplyDamage(new DamageInfoHolder(enemy.Name, enemy.TargetNode.Unit.Name, enemy, damageble, enemy.Damage));

        yield return new WaitForSeconds(1f);

        stateMachine.SetState(EnemiesStates.Idle);
    }

    public override void OnExit()
    {
    }

    public override void Tick()
    {
    }
}