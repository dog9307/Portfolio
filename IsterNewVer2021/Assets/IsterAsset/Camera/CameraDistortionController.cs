using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class CameraDistortionController : MonoBehaviour
{
    [SerializeField]
    private Volume _vol;
    private LensDistortion _dis;

    void Start()
    {
        _currentTime = _totalTime;
    }

    public void StartDistortion(float toIntensity)
    {
        // test
        toIntensity = Mathf.Clamp(toIntensity, -1.0f, 1.0f);
        StartCoroutine(Distortion(toIntensity));
    }

    public void SetScale(float toScale)
    {
        if (_dis == null)
        {
            if (_vol)
                _vol.profile.TryGet<LensDistortion>(out _dis);
        }

        _dis.scale.value = toScale;
    }

    public void SetIntensity(float toIntensity)
    {
        if (_dis == null)
        {
            if (_vol)
                _vol.profile.TryGet<LensDistortion>(out _dis);
        }

        _currentIntensity = toIntensity;
        _dis.intensity.value = _currentIntensity;
    }

    private float _totalTime = 0.5f;
    private float _currentTime;
    private float _currentIntensity;
    private int _coroutineCount = 0;
    IEnumerator Distortion(float toIntensity)
    {
        _coroutineCount++;
        _currentTime = _totalTime - _currentTime;
        float startFocal = _currentIntensity;
        while (_currentTime < _totalTime)
        {
            _currentTime += IsterTimeManager.originDeltaTime;
            float ratio = _currentTime / _totalTime;

            float intensity = Mathf.Lerp(startFocal, toIntensity, ratio);
            SetIntensity(intensity);

            yield return null;

            if (_coroutineCount > 1)
                break;
        }

        SetIntensity(toIntensity);

        _coroutineCount--;
    }

    public void ResetIntensity()
    {
        SetIntensity(0.0f);
        SetScale(1.0f);
    }
}
