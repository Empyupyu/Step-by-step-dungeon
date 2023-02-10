using System.Collections;
using UnityEngine;

public class PlayerDeathState : IState<PlayerStates>
{
    private Player player;

    public PlayerDeathState(StateMachine<PlayerStates> stateMachine, PlayerStates type, Player player) : base(stateMachine, type)
    {
        this.player = player;
    }

    public override void OnEnter()
    {
        player.Animator.SetTrigger("Death");

        ClearNode();
    }

    private void ClearNode()
    {
        player.CurrentNode.SetUnit(null);
        player.SetCurrentNode(null);
    }

    public override void OnExit() { }
    public override void Tick() { }
}