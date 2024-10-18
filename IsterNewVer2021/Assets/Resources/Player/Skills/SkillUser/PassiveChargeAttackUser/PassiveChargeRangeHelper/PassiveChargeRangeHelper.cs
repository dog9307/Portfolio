using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveChargeRangeHelper : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private Transform _end;
    [HideInInspector]
    [SerializeField]
    private Transform _endpos;
    [HideInInspector]
    [SerializeField]
    private Transform _middlePivot;

    private LookAtMouse _look;

    [SerializeField]
    private SpriteRenderer[] _sprites;

    void Start()
    {
        ApplyAlpha(0.0f);
    }

    void Update()
    {
        if (!_look)
        {
            _look = FindObjectOfType<LookAtMouse>();
            if (!_look) return;
        }

        float angle = CommonFuncs.DirToDegree(_look.dir);
        transform.rotation = Quaternion.identity;
        transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        _end.transform.position = _endpos.transform.position;
    }

    public void ApplyScale(float scaleFactor)
    {
        if (_middlePivot)
        {
            Vector3 scale = _middlePivot.localScale;
            scale.y = scaleFactor;
            _middlePivot.localScale = scale;
        }
    }

    public void StartFading(float toAlpha, float totalTime)
    {
        StartCoroutine(Fading(toAlpha, totalTime));
    }

    private int _coroutineCount = 0;
    private float _currentFadingTime = 0.0f;
    private float _currentAlpha = 0.0f;
    private float _currentRatio = 0.0f;
    IEnumerator Fading(float toAlpha, float totalTime)
    {
        _coroutineCount++;
        _currentFadingTime = Mathf.Lerp(0.0f, totalTime, 1.0f - _currentRatio);
        float startAlpha = _currentAlpha;
        while (_currentFadingTime < totalTime)
        {
            _currentFadingTime += IsterTimeManager.deltaTime;
            _currentRatio = _currentFadingTime / totalTime;

            _currentAlpha = Mathf.Lerp(startAlpha, toAlpha, _currentRatio);
            ApplyAlpha(_currentAlpha);

            yield return null;

            if (_coroutineCount > 1)
                break;
        }
        _currentFadingTime = totalTime;

        _currentAlpha = Mathf.Lerp(startAlpha, toAlpha, 1.0f);
        ApplyAlpha(_currentAlpha);

        _coroutineCount--;
    }

    void ApplyAlpha(float alpha)
    {
        foreach (var s in _sprites)
        {
            Color color = s.color;
            color.a = alpha;
            s.color = color;
        }
    }
}
