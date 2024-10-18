using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowTimeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.additionalTime += 1.0f;
    }

    public override void BuffOff()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.additionalTime -= 1.0f;
        if (user.additionalTime < 0.0f)
            user.additionalTime = 0.0f;
    }
}
