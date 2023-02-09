using Supyrb;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TeleportScreen : MonoBehaviour
{
    [SerializeField] private GameObject window;
    [SerializeField] private Button teleportButton;
    [SerializeField] private TextMeshProUGUI nextLevelText;
    [SerializeField] private Button closeWindowButton;

    private Portal selectedTeleport;

    private void Awake()
    {
        Signals.Get<PlayerOpenTeleportWindowSignal>().AddListener(OpenTeleport);

        teleportButton.onClick.AddListener(() => OnTeleportButtonClick());
        closeWindowButton.onClick.AddListener(() => EnableWindow(false));
    }

    private void OpenTeleport(Portal teleport)
    {
        EnableWindow(true);

        selectedTeleport = teleport;
        nextLevelText.text = "Level " + selectedTeleport.TransitionOnLevelIndex + 1;
    }

    private void OnTeleportButtonClick()
    {
        Signals.Get<TeleportationOnNextLevelSignal>().Dispatch(selectedTeleport);
        EnableWindow(false);
    }

    private void EnableWindow(bool value)
    {
        window.SetActive(value);
    }
}