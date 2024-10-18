using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceTimeSlow : CutSceneSqeunceBase
{
    [Header("타임 스케일 조정")]
    [SerializeField]
    private float _startSlowTime = 0.5f;
    [SerializeField]
    private float _startToTimeScale = 0.5f;

    //[SerializeField]
    //private float _endSlowTime = 0.5f;
    //[SerializeField]
    //private float _endToTimeScale = 1.0f;

    protected override IEnumerator DuringSequence()
    {
        float currentTime = 0.0f;
        float startGlobalTimeScale = 0.0f;
        float targetGlobalTimeScale = 0.0f;

        // 스케일 조정 시작 시퀀스
        currentTime = 0.0f;
        startGlobalTimeScale = IsterTimeManager.globalTimeScale;
        targetGlobalTimeScale = _startToTimeScale;
        while (currentTime < _startSlowTime)
        {
            float ratio = currentTime / _startSlowTime;

            IsterTimeManager.globalTimeScale = Mathf.Lerp(startGlobalTimeScale, targetGlobalTimeScale, ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        IsterTimeManager.globalTimeScale = targetGlobalTimeScale;

        //yield return new WaitForSeconds(_sequenceTime);

        // 스케일 조정 끝 시퀀스
        //currentTime = 0.0f;
        //startGlobalTimeScale = IsterTimeManager.globalTimeScale;
        //targetGlobalTimeScale = _endToTimeScale;
        //while (currentTime < _endSlowTime)
        //{
        //    float ratio = currentTime / _endSlowTime;

        //    IsterTimeManager.globalTimeScale = Mathf.Lerp(startGlobalTimeScale, targetGlobalTimeScale, ratio);

        //    yield return null;

        //    currentTime += IsterTimeManager.originDeltaTime;
        //}
        //IsterTimeManager.globalTimeScale = targetGlobalTimeScale;

        _isDuringSequence = false;
    }
}
