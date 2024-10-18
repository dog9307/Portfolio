using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRangeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.scaleFactor += 0.2f;
    }

    public override void BuffOff()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.scaleFactor -= 0.2f;
        if (user.scaleFactor < 1.0f)
            user.scaleFactor = 1.0f;
    }
}
