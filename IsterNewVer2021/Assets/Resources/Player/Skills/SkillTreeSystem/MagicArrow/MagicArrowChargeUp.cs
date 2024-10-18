using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowChargeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = owner as MagicArrowUser;

        user.additiveCount += 1;
        user.currentCount += 1;

        user.CoolTimeStart();
    }

    public override void BuffOff()
    {
        MagicArrowUser user = owner as MagicArrowUser;

        user.additiveCount -= 1;
        if (user.additiveCount < 0)
            user.additiveCount = 0;

        if (user.currentCount > user.maxCount)
            user.currentCount = user.maxCount;
    }
}
