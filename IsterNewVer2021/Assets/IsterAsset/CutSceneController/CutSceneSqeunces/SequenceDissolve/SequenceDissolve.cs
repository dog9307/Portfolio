using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceDissolve : CutSceneSqeunceBase
{
    [Header("디졸브")]
    [SerializeField]
    private DissolveApplier _targetDissolve;
    public DissolveApplier targetDissolve { get { return _targetDissolve; } set { _targetDissolve = value; } }
    [SerializeField]
    private float _startFade = 0.0f;
    [SerializeField]
    private float _endFade = 1.0f;

    protected override IEnumerator DuringSequence()
    {
        if (_targetDissolve)
        {
            if (Mathf.Abs(_targetDissolve.currentFade - _endFade) < float.Epsilon)
            {
                _isDuringSequence = false;
                yield break;
            }

            _targetDissolve.currentFade = _startFade;

            float currentTime = 0.0f;
            while (currentTime < _sequenceTime)
            {
                float ratio = currentTime / _sequenceTime;

                _targetDissolve.currentFade = Mathf.Lerp(_startFade, _endFade, ratio);

                yield return null;

                currentTime += IsterTimeManager.deltaTime;
            }
            _targetDissolve.currentFade = _endFade;
        }

        _isDuringSequence = false;
    }
}
