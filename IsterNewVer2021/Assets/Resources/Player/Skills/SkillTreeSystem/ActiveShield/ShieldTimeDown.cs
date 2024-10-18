using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTimeDown : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.additionalTime -= 0.5f;
    }

    public override void BuffOff()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.additionalTime += 0.5f;
        if (user.additionalTime > 0.0f)
            user.additionalTime = 0.0f;
    }
}
