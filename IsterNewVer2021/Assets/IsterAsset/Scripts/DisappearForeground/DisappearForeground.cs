using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearForeground : MonoBehaviour
{
    [SerializeField]
    private float _startAlpha = 1.0f;
    [SerializeField]
    private bool _isRotationFix = false;

    [SerializeField]
    private float _totalFadeTime = 0.15f;
    private int _coroutineCount = 0;
    private float _currentFadeTime = 0.0f;
    private float _currentAlpha = 0.0f;

    [SerializeField]
    private List<SpriteRenderer> _renderers;

    void Start()
    {
        _currentFadeTime = _totalFadeTime;

        ApplyAlpha(_startAlpha);
        _currentAlpha = _startAlpha;
    }

    void Update()
    {
        if (_isRotationFix)
            transform.rotation = Quaternion.identity;
    }

    public void StartFading(float toAlpha)
    {
        StartCoroutine(Fading(toAlpha, false));
    }

    public void StartFading(float toAlpha, bool isIgnorePrev = false)
    {
        StartCoroutine(Fading(toAlpha, isIgnorePrev));
    }

    IEnumerator Fading(float toAlpha, bool isIgnorePrev = false)
    {
        _coroutineCount++;
        if (isIgnorePrev)
            _currentFadeTime = 0.0f;
        else
            _currentFadeTime = _totalFadeTime - _currentFadeTime;
        float startAlpha = _currentAlpha;
        while (_currentFadeTime < _totalFadeTime)
        {
            float ratio = _currentFadeTime / _totalFadeTime;

            _currentAlpha = Mathf.Lerp(startAlpha, toAlpha, ratio);
            ApplyAlpha(_currentAlpha);

            yield return null;

            _currentFadeTime += IsterTimeManager.deltaTime;

            if (_coroutineCount > 1)
            {
                _coroutineCount--;
                yield break;
            }
        }
        _currentFadeTime = _totalFadeTime;

        _currentAlpha = Mathf.Lerp(startAlpha, toAlpha, 1.0f);
        ApplyAlpha(_currentAlpha);

        _coroutineCount--;
    }

    void ApplyAlpha(float alpha)
    {
        if (_renderers == null) return;

        foreach (var renderer in _renderers)
        {
            float toAlpha = alpha;

            Color color = renderer.color;
            color.a = toAlpha;
            renderer.color = color;
        }
    }
}
