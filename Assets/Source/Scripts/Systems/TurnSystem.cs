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
    }

    private void Subscribes()
    {
        onPlayerTurnIsCompletedSignal = Signals.Get<OnPlayerTurnIsCompletedSignal>();
        onNodeSelectedSignal = Signals.Get<OnNodeSelectedSignal>();
        nodeHighlightSignal = Signals.Get<NodeHighlightSignal>();
        infoSignal = Signals.Get<InfoSignal>();

        Signals.Get<PlayerCanMovmentSignal>().AddListener(PlayerDoAction);

        onNodeSelectedSignal.AddListener(PlayerTurnActivating);
        onPlayerTurnIsCompletedSignal.AddListener(UnitsTurn);
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForEndOfFrame();

        ChangeTurn();
    }

    private void PlayerTurnActivating()
    {
        Signals.Get<DisableNodeHighlightSignal>().Dispatch();
    }

    private void PlayerDoAction()
    {
        game.Player.DoAction();

        ChangeTurn();
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
        if (game.GameIsOver)
        {
            GameOverInfo();

            return;
        }

        ++game.RoundIndex;

        infoSignal.Dispatch("Раунд №" + game.RoundIndex);
        nodeHighlightSignal.Dispatch(1f);
    }

    private void GameOverInfo()
    {
        var gameOverText = game.Player.Name + " победил одолев всех врагов в Раунде №" + game.RoundIndex;

        if (game.Player.IsDeath)
        {
            gameOverText = game.Player.Name + " побиг в Раунде №" + game.RoundIndex;
        }

        infoSignal.Dispatch(gameOverText);
    }
}