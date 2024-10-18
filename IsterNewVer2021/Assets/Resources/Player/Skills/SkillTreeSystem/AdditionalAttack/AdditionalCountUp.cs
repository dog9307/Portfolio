using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalCountUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.additionalCount += 1;
        user.currentCount = user.maxCount;
    }

    public override void BuffOff()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.additionalCount -= 1;
        if (user.additionalCount < 0)
            user.additionalCount = 0;

        user.currentCount = user.maxCount;
    }
}
