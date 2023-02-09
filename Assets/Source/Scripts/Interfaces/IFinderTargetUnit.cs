public interface IFinderTargetUnit
{
    public float Radius { get; }

    public void SetRadius(float radius);
    public void FindTargetInRadius();
}