using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1SpeedBuff : TowerF1BuffItem
{
    //private float _speed;

    public override void Init()
    {
        //_speed = 5.0f;

        base.Init();

        id = 101;
    }

    public override void BuffOn()
    {
        //_buff.SpeedBuffOn(_speed);
    }

    public override void BuffOff()
    {
        //_buff.SpeedBuffOff(_speed);
    }
}
