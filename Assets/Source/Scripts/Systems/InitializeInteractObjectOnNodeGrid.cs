using System.Collections;
using UnityEngine;

public class InitializeInteractObjectOnNodeGrid : GameSystem
{
    public override void OnStart()
    {
        InitializePlayer();

        StartCoroutine(SetNearestNodeForEnemies());
    }

    private IEnumerator SetNearestNodeForEnemies()
    {
        yield return new WaitForEndOfFrame();

        InitializeEnemies();
        InitializePortals();
        InitializeChest();
    }

    private void InitializePlayer()
    {
        game.NodeGrid[0, 0].SetUnit(game.Player);
        game.Player.SetCurrentNode(game.NodeGrid[0, 0]);
    }

    private void InitializeEnemies()
    {
        for (int i = 0; i < game.Level.Enemies.Count; i++)
        {
            var enemy = game.Level.Enemies[i];

            enemy.SetMoveTimeToTargetNode(congfig.MoveTimeToTargetNode);
            enemy.SetTimeToLookAt(congfig.TimeToLookAt);

            var nodes = FindNearestNodes(enemy.transform.position, .5f);

            if (nodes.Length == 0) continue;

            var node = nodes[0].GetComponent<Node>();

            SetNodePosition(enemy.transform, node.transform.position);
            enemy.SetCurrentNode(node);
            node.SetUnit(enemy);
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

    private void InitializeChest()
    {
        for (int i = 0; i < game.Level.Chests.Count; i++)
        {
            var chest = game.Level.Chests[i];
            var nodes = FindNearestNodes(chest.transform.position, .5f);

            if (nodes.Length == 0) continue;

            SetNodePosition(chest.transform, nodes[0].transform.position);
            nodes[0].GetComponent<Node>().SetInteractlbe(chest);
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