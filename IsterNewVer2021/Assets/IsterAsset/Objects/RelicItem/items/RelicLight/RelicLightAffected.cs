using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RelicLightAffected : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.Rendering.Universal.Light2D _light;

    [SerializeField]
    private SFXPlayer _sfx;

    private bool _isAleadyAffected = false;

    [SerializeField]
    private float _scale = 3.0f;
    [SerializeField]
    private float _totalTime = 0.3f;
    [SerializeField]
    private AnimationCurve _curve;

    [SerializeField]
    private Transform _scalableTransform;

    public void AffectRelicLight()
    {
        if (_isAleadyAffected) return;

        _isAleadyAffected = true;
        _sfx.PlaySFX("lightOn");

        StartCoroutine(LightOn());
    }

    IEnumerator LightOn()
    {
        yield return new WaitForSeconds(0.2f);

        float currentTime = 0.0f;
        float startRadius = _light.pointLightOuterRadius;
        float endRadius = startRadius * _scale;
        float startIntensity = _light.intensity;
        Vector3 startScale = new Vector3(1.0f, 1.0f, 1.0f);
        Vector3 endScale = new Vector3(_scale, _scale, _scale);
        while (currentTime < _totalTime)
        {
            float ratio = currentTime / _totalTime;

            _light.pointLightOuterRadius = Mathf.Lerp(startRadius, endRadius, ratio) * _curve.Evaluate(ratio);
            _light.intensity = Mathf.Lerp(startIntensity, 1.0f, ratio);

            if (_scalableTransform)
                _scalableTransform.localScale = Vector3.Lerp(startScale, endScale, ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _light.pointLightOuterRadius = Mathf.Lerp(startRadius, endRadius, 1.0f) * _curve.Evaluate(1.0f);
        _light.intensity = Mathf.Lerp(startIntensity, 1.0f, 1.0f);

        if (_scalableTransform)
            _scalableTransform.localScale = Vector3.Lerp(startScale, endScale, 1.0f);
    }
}
