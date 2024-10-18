using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCoolTimeDown : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.coolTimeMultipler -= 0.2f;
    }

    public override void BuffOff()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.coolTimeMultipler += 0.2f;
        if (user.coolTimeMultipler > 1.0f)
            user.coolTimeMultipler = 1.0f;
    }
}
