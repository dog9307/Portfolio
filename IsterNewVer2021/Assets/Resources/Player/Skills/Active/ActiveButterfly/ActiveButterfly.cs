using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveButterfly : ActiveBase
{
    public override void Init()
    {
        base.Init();

        _type = ACTIVE.BUTTERFLY;

        skillName = "³ÚÀÇ ²Þ";
        skillDesc = "³ªºñ³ªºñ";
    }
}
