using Supyrb;
using UnityEngine;

public class TurnSystem : GameSystem
{
    [SerializeField] private int layer = 7;

    private OnPlayerTurnIsCompletedSignal onPlayerTurnIsCompletedSignal;

    private int layerAsLayerMask;

    public override void OnAwake()
    {
        layerAsLayerMask = (1 << layer);

        game.IsPlayerTurn = true;

        Subscribes();
    }

    private void Subscribes()
    {
        onPlayerTurnIsCompletedSignal = Signals.Get<OnPlayerTurnIsCompletedSignal>();
        onPlayerTurnIsCompletedSignal.AddListener(UnitsTurn);
    }

    public override void OnUpdate()
    {
        PlayerDoAction();
    }

    private void PlayerDoAction()
    {
        if (!game.IsPlayerTurn || !Input.GetMouseButtonDown(0)) return;

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out var enter))
        {
            game.MoveDirection = ray.GetPoint(enter);

            var nodes = Physics.OverlapSphere(game.MoveDirection, .1f, layerAsLayerMask);

            for (int i = 0; i < nodes.Length; i++)
            {
                game.Player.SetTargetNode(nodes[i].GetComponent<Node>());
                break;
            }

            game.Player.DoAction();

            ChangeTurn();
        }
    }

    private void UnitsTurn()
    {
        for (int i = 0; i < game.Units.Count; i++)
        {
            game.Units[i].DoAction();
        }

        ChangeTurn();
    }

    private void ChangeTurn()
    {
        game.IsPlayerTurn = !game.IsPlayerTurn;
    }
}