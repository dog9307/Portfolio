using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDumyHPUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.additionalDumyHP += 50.0f;
    }

    public override void BuffOff()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.additionalDumyHP -= 50.0f;
        if (user.additionalDumyHP < 0.0f)
            user.additionalDumyHP = 0.0f;
    }
}
