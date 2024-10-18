using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowBossSlowUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.additionalBossTimeScale += 0.05f;
    }

    public override void BuffOff()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.additionalBossTimeScale -= 0.05f;
        if (user.additionalBossTimeScale < 0.0f)
            user.additionalBossTimeScale = 0.0f;
    }
}
