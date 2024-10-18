using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDumyTimeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.additionalDumyTime += 1.0f;
    }

    public override void BuffOff()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.additionalDumyTime -= 1.0f;
        if (user.additionalDumyTime < 0.0f)
            user.additionalDumyTime = 0.0f;
    }
}
