using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceTimeSlow : CutSceneSqeunceBase
{
    [Header("Ÿ�� ������ ����")]
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

        // ������ ���� ���� ������
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

        // ������ ���� �� ������
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
