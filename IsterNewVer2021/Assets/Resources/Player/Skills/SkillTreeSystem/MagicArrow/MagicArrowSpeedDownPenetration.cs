using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowSpeedDownPenetration : SkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = ((MagicArrowUser)owner);
        user.ApplyHelper(new PenetrationArrowHelper());
    }

    public override void BuffOff()
    {
        MagicArrowUser user = ((MagicArrowUser)owner);
        user.ApplyHelper(new NormalMagicArrowHelper());
    }
}
