using DG.Tweening;
using UnityEngine;

public class ScreenAnimated : MonoBehaviour
{
    [Header("Animation Settings")]
    [SerializeField] private RectTransform _transform;
    [SerializeField] private float _travelDuration = 0.5f;
    [SerializeField] private float _returnDuration = 0.6f;
    [SerializeField] private float _delay = 0.3f;

    [Header("Fade")]
    [SerializeField] private float _fadeInTime = 0.3f;
    [SerializeField] private float _fadeOutTime = 0.6f;

    private CanvasGroup _canvasGroup;
    private Vector3 _leftOffscreen;
    private Vector3 _rightOffscreen;
    private Vector3 _center;

    void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _center = _transform.anchoredPosition;

        float canvasWidth = ((RectTransform)_transform.parent).rect.width;
        float offsetX = canvasWidth * 0.6f;

        _leftOffscreen = _center + Vector3.left * offsetX;
        _rightOffscreen = _center + Vector3.right * offsetX;
    }

    void OnEnable()
    {
        AnimateScreen();
    }

    public void AnimateScreen()
    {
        _transform.anchoredPosition = _leftOffscreen;
        gameObject.SetActive(true);

        Sequence seq = DOTween.Sequence();

        seq.Append(_canvasGroup.DOFade(1f, _fadeInTime));
        seq.Append(_transform.DOAnchorPos(_rightOffscreen, _travelDuration).SetEase(Ease.InSine));
        seq.Append(_transform.DOAnchorPos(_center, _returnDuration).SetEase(Ease.OutBack));
        seq.Join(_transform.DOScale(1.3f, _returnDuration).SetEase(Ease.OutExpo));
        seq.AppendInterval(_delay);
        seq.Join(_canvasGroup.DOFade(0f, _fadeOutTime));
        seq.AppendInterval(_delay);
        seq.OnComplete(() => { });
    }
}
