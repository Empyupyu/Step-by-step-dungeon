using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class InitializeInteractObjectOnNodeGrid : GameSystem
{
    public override void OnStart()
    {
        StartCoroutine(SetNearestNodeForEnemies());
    }

    private IEnumerator SetNearestNodeForEnemies()
    {
        yield return new WaitForEndOfFrame();

        InitializePlayer();
        InitializeEnemies();
        InitializePortals();
    }

    private void InitializePlayer()
    {
        game.NodeGrid[0, 0].SetUnit(game.Player);
        game.PlayerStayOnCurrentNode = game.NodeGrid[0, 0];
    }

    private void InitializeEnemies()
    {
        for (int i = 0; i < game.Level.Enemy.Count; i++)
        {
            var enemy = game.Level.Enemy[i];

            var nodes = FindNearestNodes(enemy.transform.position, .5f);

            if (nodes.Length == 0) continue;

            SetNodePosition(enemy.transform, nodes[0].transform.position);
            nodes[0].GetComponent<Node>().SetUnit(enemy);
        }

    }

    private void InitializePortals()
    {
        for (int i = 0; i < game.Level.Portals.Count; i++)
        {
            var portal = game.Level.Portals[i];
            var nodes = FindNearestNodes(portal.transform.position, .5f);

            if (nodes.Length == 0) continue;

            SetNodePosition(portal.transform, nodes[0].transform.position);
            nodes[0].GetComponent<Node>().SetInteractlbe(portal);
        }
    }

    private Collider[] FindNearestNodes(Vector3 position, float radius)
    {
       return Physics.OverlapSphere(position, radius, game.NodeLayerMask);
    }

    private void SetNodePosition(Transform interactObject, Vector3 nodePosition)
    {
        interactObject.transform.position = new Vector3(nodePosition.x, interactObject.transform.position.y, nodePosition.z);
    }
}