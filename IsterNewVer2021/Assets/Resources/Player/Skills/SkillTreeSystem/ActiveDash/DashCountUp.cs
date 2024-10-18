using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCountUp : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveDashUser user = owner as ActiveDashUser;
        
        user.additiveCount += 1;
        user.currentCount += 1;

        user.CoolTimeStart();
    }

    public override void BuffOff()
    {
        ActiveDashUser user = owner as ActiveDashUser;

        user.additiveCount -= 1;
        if (user.additiveCount < 0)
            user.additiveCount = 0;

        if (user.currentCount > user.maxCount)
            user.currentCount = user.maxCount;
    }
}
