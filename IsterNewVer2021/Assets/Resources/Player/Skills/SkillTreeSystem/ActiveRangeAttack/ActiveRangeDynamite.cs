using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRangeDynamite : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.delayTime += 1.0f;
    }

    public override void BuffOff()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.delayTime -= 1.0f;
        if (user.delayTime < 0.0f)
            user.delayTime = 0.0f;
    }
}
