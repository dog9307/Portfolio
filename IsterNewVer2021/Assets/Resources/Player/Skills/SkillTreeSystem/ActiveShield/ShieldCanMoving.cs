using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCanMoving : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.isCanMoving = true;
        user.speed += 1.0f;
    }

    public override void BuffOff()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.speed -= 1.0f;
        if (user.speed < 0.0f)
        {
            user.isCanMoving = false;
            user.speed = 0.0f;
        }
    }
}
