using Supyrb;
using UnityEngine;

public class Portal : MonoBehaviour, IInteractble
{
    [field : SerializeField] public int TransitionOnLevelIndex { get; private set; }

    public void Interact()
    {
        Signals.Get<PlayerOpenTeleportWindowSignal>().Dispatch(this);
    }
}