using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashCoolTimeDown : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.coolTimeMultipler -= 0.1f;
    }

    public override void BuffOff()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.coolTimeMultipler += 0.1f;
        if (user.coolTimeMultipler > 1.0f)
            user.coolTimeMultipler = 1.0f;
    }
}
