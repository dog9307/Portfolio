using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTorchTalkFrom : TalkFrom
{
    [SerializeField]
    private PuzzleTorchFloor[] _floors;

    [SerializeField]
    private ParticleSystem _effect;

    void Start()
    {
        _effect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    public override void Talk()
    {
        if (_effect.isPlaying)
            _effect.Stop();
        else
            _effect.Play();

        foreach (var f in _floors)
            f.Active();

        if (_sfx)
            _sfx.PlaySFX("active");
    }
}
