using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceParticleEffector : CutSceneSqeunceBase
{
    [Header("파티클")]
    [SerializeField]
    private ParticleSystem _effect;

    protected override IEnumerator DuringSequence()
    {
        if (!_effect)
        {
            _isDuringSequence = false;
            yield break;
        }

        if (!_effect.isPlaying)
        {
            _effect.gameObject.SetActive(true);
            _effect.Play();
        }

        yield return new WaitForSeconds(_sequenceTime);

        if (_effect.isPlaying)
            _effect.Stop();

        _isDuringSequence = false;
    }
}
