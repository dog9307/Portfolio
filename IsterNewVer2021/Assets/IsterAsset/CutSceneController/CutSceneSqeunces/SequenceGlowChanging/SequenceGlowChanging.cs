using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceGlowChanging : CutSceneSqeunceBase
{
    [Header("글로우")]
    [SerializeField]
    private GlowableObject _glow;
    [SerializeField]
    private AnimationCurve _curve;

    protected override IEnumerator DuringSequence()
    {
        float currentTime = 0.0f;
        while (currentTime < _sequenceTime)
        {
            float ratio = currentTime / _sequenceTime;
            float value = _curve.Evaluate(ratio);

            _glow.SetIntensity(value);

            yield return null;

            currentTime += IsterTimeManager.deltaTime;
        }

        _isDuringSequence = false;
    }
}
