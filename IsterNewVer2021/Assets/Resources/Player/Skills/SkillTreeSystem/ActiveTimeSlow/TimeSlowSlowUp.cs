using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowSlowUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.additionalEnemyTimeScale += 0.1f;
    }

    public override void BuffOff()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.additionalEnemyTimeScale -= 0.1f;
        if (user.additionalEnemyTimeScale < 0.0f)
            user.additionalEnemyTimeScale = 0.0f;
    }
}
