using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTimeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.additionalTime += 1.0f;
    }

    public override void BuffOff()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.additionalTime -= 1.0f;
        if (user.additionalTime < 0.0f)
            user.additionalTime = 0.0f;
    }
}
