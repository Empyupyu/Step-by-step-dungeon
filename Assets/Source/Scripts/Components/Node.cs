using UnityEngine;

public class Node : MonoBehaviour
{
    public Unit Unit { get; private set; }

    public void SetUnit(Unit unit)
    {
        Unit = unit;
    }
}