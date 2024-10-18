using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveReversePower : ActiveBase
{
    public float figure
    {
        get
        {
            // test
            return 1;
        }
        set { }
    }

    public override void Init()
    {
        base.Init();

        _type = ACTIVE.REVERSEPOWER;

        skillName = "반전강화 : P";
        skillDesc = "스킬 사용 시 다음 공격때 사용되는 패시브 스킬들이 강화된다.";
    }
}
