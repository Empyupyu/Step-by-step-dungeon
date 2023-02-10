using Supyrb;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeathScreen : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private RectTransform deathWindow;

    private void Awake()
    {
        restartButton.onClick.AddListener(Restart);
        Signals.Get<OpenGameOverWindowSignal>().AddListener(Open);
    }

    private void Restart()
    {
        Signals.Get<RestartLevelSignal>().Dispatch();
    }

    private void Open()
    {
        deathWindow.gameObject.SetActive(true);
    }
}