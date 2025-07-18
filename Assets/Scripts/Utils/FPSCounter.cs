using UnityEngine;
using TMPro;

public class FPSCounter : Singleton<FPSCounter>
{
    
    [Header("Settings")]
    [SerializeField] private float refreshInterval = 0.5f;

    [Header("UI")]
    [SerializeField] private TMP_Text fpsText;

    private int _curremtFramesCount = 0;
    private float _currentFramesTime = 0f;

    void Update()
    {
        _curremtFramesCount++;
        _currentFramesTime += Time.unscaledDeltaTime;

        if (_currentFramesTime >= refreshInterval)
        {
            var fps = _curremtFramesCount / _currentFramesTime;
            UpdateDisplay(fps);

            _curremtFramesCount = 0;
            _currentFramesTime = 0f;
        }
    }

    private void UpdateDisplay(float fps)
    {
        fpsText.text = $"FPS: {Mathf.RoundToInt(fps)}";

        if (fps >= 50)
            fpsText.color = Color.green;
        else if (fps >= 30)
            fpsText.color = Color.yellow;
        else
            fpsText.color = Color.red;
    }
}