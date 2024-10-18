using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowJumpForTime : BossAttackBase
{
    Coroutine _coroutine;
    public override void SetPatternId()
    {
    }
    public override void PatternStart()
    {
        base.PatternStart();
    }
    public override void PatternOn()
    {
        //_currentCount = 0;
        //_currentTime = 0;
        //_coroutine = StartCoroutine(Jump());
    }
    public override void PatternOff()
    {
        _owner.attackPatternEnd = true;
    }
    public override void PatternEnd()
    {
        base.PatternEnd();

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
}