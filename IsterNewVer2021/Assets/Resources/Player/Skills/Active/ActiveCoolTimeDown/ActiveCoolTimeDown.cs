using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoolTimeDown : ActiveBase
{
    public ActiveCoolTimeDownUser user { get; set; }
    public float figure { get { return 10.0f; } }

    public override void Init()
    {
        base.Init();
        _type = ACTIVE.COOLTIMEDOWN;

        skillName = "쿨타임 감소(A)";
        skillDesc = "남은 쿨타임을 x초 감소한다.";
    }
}
