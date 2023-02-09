using DG.Tweening;
using Supyrb;
using UnityEngine;

public class Player : Unit
{
    public Node TargetNode { get; private set; }
    private OnPlayerTurnIsCompletedSignal onPlayerTurnIsCompletedSignal;

    private void Awake()
    {
        onPlayerTurnIsCompletedSignal = Signals.Get<OnPlayerTurnIsCompletedSignal>();
    }

    public void SetTargetNode(Node node)
    {
        TargetNode = node;
    }

    public override void DoAction()
    {
        if(TargetNode.Unit == null)
        {
            //move state

            var moveDirection = TargetNode.transform.position;
            var movePoint = new Vector3(moveDirection.x, transform.position.y, moveDirection.z);
            transform.DOLookAt(movePoint, .5f);
            transform.DOMove(movePoint, 1.5f).OnComplete(() => 
            {
                Signals.Get<InfoSignal>().Dispatch(Name + " Сделал шаг");
                onPlayerTurnIsCompletedSignal.Dispatch();
            });
        }
        else  if(TargetNode.Unit != null)
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