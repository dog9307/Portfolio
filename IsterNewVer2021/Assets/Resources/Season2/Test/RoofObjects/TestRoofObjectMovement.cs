using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoofObjectMovement : MonoBehaviour
{
    [SerializeField]
    private float _ratio = 0.1f;

    private Vector3 _camStartPos;
    private Vector3 _startPos;

    private Vector3 _moveOffset;

    [SerializeField]
    private bool _isXReverse = false;
    [SerializeField]
    private bool _isYReverse = true;

    private bool _isVisible;

    [SerializeField]
    private SpriteRenderer _renderer;
    private float _maxAlpha;
    private float _currentAlpha;
    private float _currentToAlpha;
    private Coroutine _fadeCoroutine;

    void Start()
    {
        if (!_renderer)
            _renderer = GetComponent<SpriteRenderer>();

        if (_renderer)
        {
            _maxAlpha = _renderer.color.a;
            _currentAlpha = 0.0f;
            ApplyAlpha(_currentAlpha);
        }
    }

    private void OnDisable()
    {
        if (_fadeCoroutine != null)
        {
            _currentAlpha = _currentToAlpha;
            ApplyAlpha(_currentAlpha);
            StopCoroutine(_fadeCoroutine);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isVisible) return;

        _moveOffset = Camera.main.transform.position - _camStartPos;

        Vector3 offset = _moveOffset;
        if (_isXReverse)
            offset.x *= -1;
        if (_isYReverse)
            offset.y *= -1;

        transform.position = _startPos + offset * _ratio;
    }

    private void OnBecameVisible()
    {
        _isVisible = true;

        _startPos = transform.position;

        _camStartPos = Camera.main.transform.position;

        if (_renderer && gameObject.activeInHierarchy)
        {
            if (_fadeCoroutine != null)
            {
                _currentAlpha = _currentToAlpha;
                ApplyAlpha(_currentAlpha);
                StopCoroutine(_fadeCoroutine);
            }

            _fadeCoroutine = StartCoroutine(Fade(_maxAlpha));
        }
    }

    private void OnBecameInvisible()
    {
        _isVisible = false;

        transform.position = _startPos;

        if (_renderer && gameObject.activeInHierarchy)
        {
            if (_fadeCoroutine != null)
            {
                _currentAlpha = _currentToAlpha;
                ApplyAlpha(_currentAlpha);
                StopCoroutine(_fadeCoroutine);
            }

            _fadeCoroutine = StartCoroutine(Fade(0.0f));
        }
    }

    IEnumerator Fade(float toAlpha)
    {
        _currentToAlpha = toAlpha;

        float currentTime = 0.0f;
        float totalTime = 1.0f;
        float startAlpha = _currentAlpha;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            _currentAlpha = Mathf.Lerp(startAlpha, toAlpha, ratio);
            ApplyAlpha(_currentAlpha);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _currentAlpha = toAlpha;
        ApplyAlpha(_currentAlpha);
    }

    void ApplyAlpha(float alpha)
    {
        Color color = _renderer.color;
        color.a = alpha;
        _renderer.color = color;
    }
}
