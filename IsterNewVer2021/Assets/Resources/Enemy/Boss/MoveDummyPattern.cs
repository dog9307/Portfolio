using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDummyPattern : BossMoveBase
{
    public override void PatternOn()
    {
        //_movable._moveStart = false;
       PatternOff();
    }

    public override void PatternOff()
    {
        //_movable._moveStart = false;
        _owner.movePatternEnd = true;
    }
}
