using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDumyRangeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.scaleFactor += 0.3f;
    }

    public override void BuffOff()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.scaleFactor -= 0.3f;
        if (user.scaleFactor < 1.0f)
            user.scaleFactor = 1.0f;
    }
}
