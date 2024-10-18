using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceTargetScaleChanging : CutSceneSqeunceBase
{
    [Header("스케일")]
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float _minScale = 0.0f;
    [SerializeField]
    private float _maxScale = 1.0f;

    [SerializeField]
    private bool _isXScale = true;
    [SerializeField]
    private bool _isYScale = false;
    [SerializeField]
    private bool _isZScale = false;

    protected override IEnumerator DuringSequence()
    {
        ApplyScale(_minScale);
        float currentTime = 0.0f;
        Vector3 scale = _target.localScale;
        float scaleFactor = _minScale;
        while (currentTime < _sequenceTime)
        {
            float ratio = currentTime / _sequenceTime;
            scaleFactor = Mathf.Lerp(_minScale, _maxScale, ratio);

            ApplyScale(scaleFactor);

            yield return null;

            currentTime += IsterTimeManager.deltaTime;
        }
        ApplyScale(_maxScale);

        _isDuringSequence = false;
    }

    public void ApplyScale(float scaleFactor)
    {
        Vector3 scale = _target.localScale;

        scale.x = (_isXScale ? scaleFactor : scale.x);
        scale.y = (_isYScale ? scaleFactor : scale.y);
        scale.z = (_isZScale ? scaleFactor : scale.z);

        _target.localScale = scale;
    }
}
