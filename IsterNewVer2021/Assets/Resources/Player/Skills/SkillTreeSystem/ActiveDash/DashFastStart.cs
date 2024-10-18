using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashFastStart : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.startNormalizedTime += 0.1f;
    }

    public override void BuffOff()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.startNormalizedTime -= 0.1f;
        if (user.startNormalizedTime < 0.0f)
            user.startNormalizedTime = 0.0f;
    }
}
