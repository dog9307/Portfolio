using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1LowVisible : TowerF1BuffItem
{
    private float _visibleRatio;

    public override void Init()
    {
        //_visibleRatio = 0.1f;

        base.Init();

        id = 102;
    }

    public override void BuffOn()
    {
        //_buff.enemyVisibleRatio = _visibleRatio;
    }

    public override void BuffOff()
    {
        //_buff.enemyVisibleRatio = 1.0f;
    }
}
