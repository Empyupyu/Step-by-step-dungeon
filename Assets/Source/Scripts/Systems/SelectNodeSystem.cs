using Supyrb;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectNodeSystem : GameSystem
{
    private OnNodeSelectedSignal onNodeSelectedSignal;

    public override void OnAwake()
    {
        onNodeSelectedSignal = Signals.Get<OnNodeSelectedSignal>();
    }

    public override void OnUpdate()
    {
        TrySelectingNode();
    }

    private void TrySelectingNode()
    {
        if (!CanTrySelecting()) return;

        CreateRaycast();
        SelectNode();
    }

    private bool CanTrySelecting()
    {
        return game.IsPlayerTurn && Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject();
    }

    private void CreateRaycast()
    {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out var enter))
        {
            game.MoveDirection = ray.GetPoint(enter);
        }
    }

    private void SelectNode()
    {
        var nodes = Physics.OverlapSphere(game.MoveDirection, .1f, game.NodeLayerMask);

        if (nodes.Length == 0) return;

        var node = nodes[0].GetComponent<Node>();

        if (game.Player.CurrentNode.Equals(node)) return;
        if (!AvailableNodes(node)) return;

        game.Player.SetTarget(node);

        onNodeSelectedSignal.Dispatch();
    }

    private bool AvailableNodes(Node selectNode)
    {
        return game.AvailableNodes.Find(n => n.Equals(selectNode));
    }
}