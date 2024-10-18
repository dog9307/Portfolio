using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class REnemyMovePatternBase : RPatternBase
{
    [HideInInspector]
    public MP moveType;

    protected GameObject _target;

    [HideInInspector]
    public float _range;


    public override void Init()
    {
        _patternEnd = false;
        if (_owner) _range = (_owner._moveRange );
    }
    public override void Update()
    {
        if (_owner._inRange) PatternEnd();
    }

    public override void PatternStart()
    {
        
    }
    public override void PatternEnd()
    {
        //_patternEnd = true;
    }

}
