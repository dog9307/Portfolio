using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraBlurController : MonoBehaviour
{
    [SerializeField]
    private Volume _vol;
    private DepthOfField _dof;

    void Start()
    {
        _currentTime = _totalTime;
    }

    public void StartBlur(float toBlur)
    {
        // test
        toBlur = toBlur >= 40.0f ? 150.0f : 0.0f;
        StartCoroutine(Blur(toBlur));
    }

    public void SetBlur(float toBlur)
    {
        if (_dof == null)
        {
            if (_vol)
                _vol.profile.TryGet<DepthOfField>(out _dof);
        }

        _currentFocalLength = toBlur;
        _dof.focalLength.value = _currentFocalLength;
    }

    private float _totalTime = 0.5f;
    private float _currentTime;
    private float _currentFocalLength;
    private int _coroutineCount = 0;
    IEnumerator Blur(float toBlur)
    {
        _coroutineCount++;
        _currentTime = _totalTime - _currentTime;
        float startFocal = _currentFocalLength;
        while (_currentTime < _totalTime)
        {
            _currentTime += IsterTimeManager.originDeltaTime;
            float ratio = _currentTime / _totalTime;

            float blur = Mathf.Lerp(startFocal, toBlur, ratio);
            SetBlur(blur);

            yield return null;

            if (_coroutineCount > 1)
                break;
        }

        SetBlur(toBlur);

        _coroutineCount--;
    }
}
