public interface IMovable
{
    public Node TargetNode { get; }
    public Node CurrentNode { get; }

    public void SetTarget(Node node);
    public void SetCurrentNode(Node node);
}