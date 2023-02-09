using System.Collections.Generic;

public interface IMovable
{
    public float MoveTimeToTargetNode { get; }
    public float TimeToLookAt { get; }
    public float AvailableRadius { get; }
    public Node TargetNode { get; }
    public Node CurrentNode { get; }
    public List<Node> AvailableNodes { get; }

    public void SetTarget(Node node);
    public void SetCurrentNode(Node node);
    public void SetMoveTimeToTargetNode(float time);
    public void SetTimeToLookAt(float time);
    public void SetAvailableRadius(float radius);
    public void SetAvailableNodes(List<Node> availableNodes);
    public void ClearAvailableNodes();
}