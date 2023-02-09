using System.Collections;
using Supyrb;
using UnityEngine;

public class TurnSystem : GameSystem
{
    private OnPlayerTurnIsCompletedSignal onPlayerTurnIsCompletedSignal;
    private OnNodeSelectedSignal onNodeSelectedSignal;
    private NodeHighlightSignal nodeHighlightSignal;
    private InfoSignal infoSignal;

    public override void OnAwake()
    {
        Subscribes();
        StartCoroutine(DelayStart());

        InitializeEnemyUnits();
    }

    private void InitializeEnemyUnits()
    {
        for (int i = 0; i < game.Level.Enemy.Count; i++)
        {
            game.Units.Add(game.Level.Enemy[i]);
        }
    }

    private void Subscribes()
    {
        onPlayerTurnIsCompletedSignal = Signals.Get<OnPlayerTurnIsCompletedSignal>();
        onNodeSelectedSignal = Signals.Get<OnNodeSelectedSignal>();
        nodeHighlightSignal = Signals.Get<NodeHighlightSignal>();
        infoSignal = Signals.Get<InfoSignal>();

        onNodeSelectedSignal.AddListener(PlayerTurn);
        onPlayerTurnIsCompletedSignal.AddListener(UnitsTurn);
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForEndOfFrame();

        ChangeTurn();
    }

    private void PlayerTurn()
    {
        game.Player.DoAction();

        ChangeTurn();

        Signals.Get<DisableNodeHighlightSignal>().Dispatch();
    }

    private void UnitsTurn()
    {
        for (int i = 0; i < game.Units.Count; i++)
        {
            game.Units[i].DoAction();
        }

        StartCoroutine(DelayBeforeEndTurn());
    }

    private IEnumerator DelayBeforeEndTurn()
    {
        yield return new WaitForSeconds(congfig.DelayTransitionToPlayerTurn);

        ChangeTurn();
    }

    private void ChangeTurn()
    {
        game.IsPlayerTurn = !game.IsPlayerTurn;

        if (game.IsPlayerTurn) NextRaund();
    }

    private void NextRaund()
    {
        ++game.RoundIndex;

        infoSignal.Dispatch("Round ¹" + game.RoundIndex);
        nodeHighlightSignal.Dispatch(1f);
    }
}