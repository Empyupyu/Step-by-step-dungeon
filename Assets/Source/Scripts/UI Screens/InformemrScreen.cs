using DG.Tweening;
using Supyrb;
using TMPro;
using UnityEngine;

public class InformemrScreen : MonoBehaviour
{
    [SerializeField] private RectTransform infoContext;
    [SerializeField] private TextMeshProUGUI infoText;

    private void Awake()
    {
        Signals.Get<InfoSignal>().AddListener(WriteInfoText);
    }

    private void WriteInfoText(string text)
    {
        var info = Instantiate(infoText, infoContext);
        info.text = text;

        infoContext.DOMoveY(infoContext.rect.height * 2, 2);
    }
}