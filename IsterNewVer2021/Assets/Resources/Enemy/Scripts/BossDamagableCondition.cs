using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamagableCondition : DamagableCondition
{
    private bool _isPhaseChanging = false;
    public bool isPhaseChanging { get { return _isPhaseChanging; } set { _isPhaseChanging = value; } }

    public override bool IsCanHitted()
    {
        return (!_isPhaseChanging);
    }
}
