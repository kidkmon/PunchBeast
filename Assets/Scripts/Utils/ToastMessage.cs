using UnityEngine;
using TMPro;
using DG.Tweening;

public class ToastMessage : MonoBehaviour
{
    [Header("Toast Settings")]
    [SerializeField] private TextMeshProUGUI _messageText;
    [SerializeField] private CanvasGroup _canvasGroup;

    public static ToastMessage Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _canvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    public void Show(string message, float duration = 1.5f)
    {
        _messageText.text = message;
        _canvasGroup.alpha = 0;
        gameObject.SetActive(true);

        _canvasGroup.DOFade(1, 0.3f)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(duration, () =>
                {
                    _canvasGroup.DOFade(0, 0.3f)
                        .OnComplete(() => gameObject.SetActive(false));
                });
            });
    }
}
