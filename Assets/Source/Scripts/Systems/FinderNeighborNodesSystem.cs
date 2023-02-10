using System.Collections.Generic;
using DG.Tweening;
using Supyrb;
using UnityEngine;

public class FinderNeighborNodesSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<FindNeighborNodesSignal>().AddListener(FindNeighborNodes);
    }

    private void FindNeighborNodes(IMovable movable)
    {
        var nearestNodes = Physics.OverlapSphere(movable.CurrentNode.transform.position, movable.AvailableRadius, game.NodeLayerMask);
        var availableNodes = new List<Node>();

        for (int i = 0; i < nearestNodes.Length; i++)
        {
            var node = nearestNodes[i].GetComponent<Node>();

            if (node.Unit != null)
            {
                if (node.Unit.Equals(movable))
                {
                    continue;
                }
            }

            availableNodes.Add(node);
        }

        movable.SetAvailableNodes(availableNodes);
    }
}