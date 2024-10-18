using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceUIAlphaChanging : CutSceneSqeunceBase
{
    [Header("UI 관련")]
    [SerializeField]
    private FadingGuideUI _fading;

    [SerializeField]
    private float _startAlpha = 0.0f;
    [SerializeField]
    private float _endAlpha = 1.0f;

    [SerializeField]
    private float _fadingDelayTime = 5.0f;

    [SerializeField]
    private bool _isNextByKey = true;
    private bool _isNext;

    [SerializeField]
    private bool _isApplySequenceTime = false;

    void Update()
    {
        if (!_isNextByKey) return;
        if (_isNext) return;

        if (Input.anyKeyDown)
            _isNext = true;
    }

    protected override IEnumerator DuringSequence()
    {
        _isNext = false;

        if (!_fading)
        {
            _isDuringSequence = false;
            yield break;
        }

        if (_isApplySequenceTime)
            _fading.fadeDuration = _sequenceTime;

        //_fading.ApplyAlpha(_startAlpha);
        _fading.StartFading(_endAlpha);

        yield return new WaitForSeconds(_fading.totalFadeTime + _fadingDelayTime);

        if (_isNextByKey)
        {
            while (!_isNext)
                yield return null;
        }
        _isDuringSequence = false;
    }
}
