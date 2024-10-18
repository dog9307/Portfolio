using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTimeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.additionalTime += 1.0f;
    }

    public override void BuffOff()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.additionalTime -= 1.0f;
        if (user.additionalTime < 0.0f)
            user.additionalTime = 0.0f;
    }
}
