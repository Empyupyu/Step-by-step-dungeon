public interface IMovable
{
    public Node TargetNode { get; }

    public void SetTarget(Node node);
}