using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [field : SerializeField] public string Name { get; protected set; }
    [field : SerializeField] public Animator Animator { get; protected set; }

    public abstract void DoAction();
}