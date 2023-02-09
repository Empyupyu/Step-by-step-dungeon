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
        var nearestNodes = Physics.OverlapSphere(game.Player.transform.position, radius, game.NodeLayerMask);

        for(int i = 0; i < nearestNodes.Length; i++)
        {
            var node = nearestNodes[i].GetComponent<Node>();

            if (node.Unit != null && node.Unit.Equals(game.Player)) continue; 

            game.AvailableNodes.Add(node);
            game.AvailableNodes[game.AvailableNodes.Count - 1].Material.DOColor(congfig.HightlightColor, "_Color",.5f);
        }
    }

    private void DisableHigtlight()
    {
        for (int i = 0; i < game.AvailableNodes.Count; i++)
        {
            game.AvailableNodes[i].Material.DOColor(congfig.DefaultColor, .5f);
        }

        game.AvailableNodes.Clear();
    }
}