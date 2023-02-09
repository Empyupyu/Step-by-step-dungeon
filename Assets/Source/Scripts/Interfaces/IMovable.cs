public interface IMovable
{
    public float MoveTimeToTargetNode { get; }
    public float TimeToLookAt { get; }
    public Node TargetNode { get; }
    public Node CurrentNode { get; }

    public void SetTarget(Node node);
    public void SetCurrentNode(Node node);
    public void SetMoveTimeToTargetNode(float time);
    public void SetTimeToLookAt(float time);
}