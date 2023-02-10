using Supyrb;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractble
{
    [SerializeField] private Animator animator;

    public ChestData ChestData { get; private set; }

    public void SetData(ChestData chestData)
    {
        ChestData = chestData;
    }

    public void Interact()
    {
        animator.Play("Open");
        Signals.Get<OpenChestSignal>().Dispatch(this);
    }
}
