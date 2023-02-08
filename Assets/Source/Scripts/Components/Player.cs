using DG.Tweening;
using Supyrb;
using UnityEngine;

public class Player : Unit
{
    private Node targetNode;
    private OnPlayerTurnIsCompletedSignal onPlayerTurnIsCompletedSignal;

    private void Awake()
    {
        onPlayerTurnIsCompletedSignal = Signals.Get<OnPlayerTurnIsCompletedSignal>();
    }

    public void SetTargetNode(Node node)
    {
        targetNode = node;
    }

    public override void DoAction()
    {
        if(targetNode.Unit == null)
        {
            //move state

            var moveDirection = targetNode.transform.position;
            var movePoint = new Vector3(moveDirection.x, transform.position.y, moveDirection.z);
            transform.DOLookAt(movePoint, .5f);
            transform.DOMove(movePoint, 1.5f).OnComplete(() => onPlayerTurnIsCompletedSignal.Dispatch());
        }
        else  if(targetNode.Unit != null)
        {
             //attack state
        }
        else
        {
            //open chest
        }

        //onPlayerTurnIsCompletedSignal.Dispatch();
    }
}