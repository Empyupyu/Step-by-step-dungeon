using UnityEngine;

public class Node : MonoBehaviour
{
    public Material Material { get; private set; }
    public Unit Unit { get; private set; }

    private void Awake()
    {
        Material = GetComponent<MeshRenderer>().material;
    }

    public void SetUnit(Unit unit)
    {
        Unit = unit;
    }
}