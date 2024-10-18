using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBasicMove : BossMoveBase
{
    
    // Start is called before the first frame update
    public override void PatternOn()
    {
        _movable._moveStart = false;
        //_coroutine = StartCoroutine(WarpAndAttack());
    }
    public override void PatternOff()
    {
        _owner.movePatternEnd = true;
    }
    public override void PatternEnd()
    {
        base.PatternEnd();

        if (_sfx)
            _sfx.PlaySFX(_patternEndSfx);
    }
}
