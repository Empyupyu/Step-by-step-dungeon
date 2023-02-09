using System.Collections;
using Supyrb;
using UnityEngine;

public class TeleportSystem : GameSystem
{
    public override void OnAwake()
    {
        Signals.Get<TeleportationOnNextLevelSignal>().AddListener(Teleportation);
        Signals.Get<PlayerOpenTeleportWindowSignal>().AddListener(TeleportInfo);
    }

    public override void OnStart()
    {
        StartCoroutine(InitializePortalsOnNode());
    }

    private IEnumerator InitializePortalsOnNode()
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < game.Level.Portals.Count; i++)
        {
            var portal = game.Level.Portals[i];
            var nodes = Physics.OverlapSphere(portal.transform.position, .5f, game.NodeLayerMask);

            if (nodes.Length > 0)
            {
                portal.transform.position = new Vector3(nodes[0].transform.position.x, portal.transform.position.y, nodes[0].transform.position.z);
                nodes[0].GetComponent<Node>().SetInteractlbe(portal);
            }
        }
    }

    private void TeleportInfo(Portal teleport)
    {
        Signals.Get<InfoSignal>().Dispatch($"{game.Player.Name} прикоснулся к {nameof(teleport)}...");
    }

    private void Teleportation(Portal teleport)
    {
        player.LevelIndex = teleport.TransitionOnLevelIndex;
    }
}