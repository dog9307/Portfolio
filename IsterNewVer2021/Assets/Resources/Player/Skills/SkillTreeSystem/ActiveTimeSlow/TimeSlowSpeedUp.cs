using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowSpeedUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.speedUpFigure += 1.0f;

        if (!user.isSpeedUp)
            user.isSpeedUp = true;
    }

    public override void BuffOff()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.speedUpFigure -= 1.0f;
        if (user.speedUpFigure < 0.0f)
        {
            user.speedUpFigure = 0.0f;
            user.isSpeedUp = false;
        }
    }
}
