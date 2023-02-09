using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Supyrb;
using UnityEngine;

public class NodeHighlightSystem : GameSystem
{
    private List<Node> nodes = new List<Node>();

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

            nodes.Add(node);
            nodes[nodes.Count - 1].Material.DOColor(congfig.HightlightColor, "_Color",.5f);
        }
    }

    private void DisableHigtlight()
    {
        for (int i = 0; i < nodes.Count; i++)
        {
            nodes[i].Material.DOColor(congfig.DefaultColor, .5f);
        }

        nodes.Clear();
    }
}