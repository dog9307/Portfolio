using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceDistortion : CutSceneSqeunceBase
{
    [Header("¿Ö°î")]
    [SerializeField]
    private AnimationCurve _intensityCurve;

    protected override IEnumerator DuringSequence()
    {
        CameraDistortionController distortion = FindObjectOfType<CameraDistortionController>();
        if (!distortion) yield break;

        float totalTime = _sequenceTime * 0.7f;
        float currentTime = 0.0f;
        float intensity = _intensityCurve.Evaluate(0.0f);
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            intensity = _intensityCurve.Evaluate(ratio);
            distortion.SetIntensity(intensity);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        intensity = _intensityCurve.Evaluate(1.0f);
        distortion.SetIntensity(intensity);

        totalTime = _sequenceTime * 0.3f;
        float scale = 1.0f;
        currentTime = 0.0f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            scale = Mathf.Lerp(1.0f, 0.01f, ratio);
            distortion.SetScale(scale);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        distortion.SetScale(0.01f);

        _isDuringSequence = false;
    }
}
