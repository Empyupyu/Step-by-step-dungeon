using System.Collections;
using DG.Tweening;
using Supyrb;
using UnityEngine;

public class NodeHighlightSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<NodeHighlightSignal>().AddListener(Highlighting);
        Signals.Get<DisableNodeHighlightSignal>().AddListener(DisableHigtlight);
    }

    private void Highlighting(float radius)
    {
        Signals.Get<FindNeighborNodesSignal>().Dispatch(game.Player);

        game.Player.AvailableNodes.ForEach(n => n.Material.DOColor(congfig.HightlightColor, "_Color", congfig.HighlightingDuration));
    }

    private void DisableHigtlight()
    {
        game.Player.AvailableNodes.ForEach(n => n.Material.DOColor(congfig.DefaultColor, "_Color", congfig.DefaultColorDuration));
        game.Player.ClearAvailableNodes();

        Signals.Get<PlayerCanMovmentSignal>().Dispatch();
    }
}