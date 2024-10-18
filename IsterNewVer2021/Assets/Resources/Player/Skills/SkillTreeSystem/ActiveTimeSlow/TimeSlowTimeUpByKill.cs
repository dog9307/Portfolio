using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowTimeUpByKill : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.isTimeUpByKill = true;
    }

    public override void BuffOff()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.isTimeUpByKill = false;
    }
}
