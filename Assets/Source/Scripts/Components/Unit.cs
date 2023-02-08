using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [field : SerializeField] public string Name { get; protected set; }
}