using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowRockman : SkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        user.ApplyHelper(new MagicArrowChargingHelper());
    }

    public override void BuffOff()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        user.ApplyHelper(new NormalMagicArrowHelper());
    }
}
