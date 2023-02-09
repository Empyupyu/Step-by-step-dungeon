using System.Collections;
using Supyrb;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnSystem : GameSystem
{
    private OnPlayerTurnIsCompletedSignal onPlayerTurnIsCompletedSignal;
    private InfoSignal infoSignal;

    public override void OnAwake()
    {
        Subscribes();
        StartCoroutine(DelayStart());
    }

    private void Subscribes()
    {
        onPlayerTurnIsCompletedSignal = Signals.Get<OnPlayerTurnIsCompletedSignal>();
        infoSignal = Signals.Get<InfoSignal>();
        onPlayerTurnIsCompletedSignal.AddListener(UnitsTurn);
    }

    private IEnumerator DelayStart()
    {
        yield return new WaitForEndOfFrame();
        ChangeTurn();
    }

    public override void OnUpdate()
    {
        PlayerDoAction();
    }

    private void PlayerDoAction()
    {
        if (!game.IsPlayerTurn || !Input.GetMouseButtonDown(0) || EventSystem.current.IsPointerOverGameObject()) return;

        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (plane.Raycast(ray, out var enter))
        {
            game.MoveDirection = ray.GetPoint(enter);

            var nodes = Physics.OverlapSphere(game.MoveDirection, .1f, game.NodeLayerMask);

            if (nodes.Length == 0) return;

            for (int i = 0; i < nodes.Length; i++)
            {
                //reset last node
                game.PlayerStayOnCurrentNode.SetUnit(null);
                game.PlayerStayOnCurrentNode = nodes[i].GetComponent<Node>();

                game.Player.SetTargetNode(game.PlayerStayOnCurrentNode);
                break;
            }

            game.Player.DoAction();
            
            game.PlayerStayOnCurrentNode.SetUnit(game.Player);

            ChangeTurn();

            Signals.Get<DisableNodeHighlightSignal>().Dispatch();
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

        if (game.IsPlayerTurn) NextRaund();
    }

    private void NextRaund()
    {
        ++game.RoundIndex;

        infoSignal.Dispatch("Round ¹" + game.RoundIndex);
        Signals.Get<NodeHighlightSignal>().Dispatch(1f);
    }
}