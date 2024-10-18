using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveYaran : ActiveBase
{
    public override void Init()
    {
        base.Init();

        _type = ACTIVE.YARAN;

        skillName = "야란";
        skillDesc = "야란 원소전투스킬";
    }
}
