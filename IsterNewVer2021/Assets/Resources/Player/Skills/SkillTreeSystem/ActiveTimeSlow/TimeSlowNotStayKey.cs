using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSlowNotStayKey : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.isStayKey = false;
    }

    public override void BuffOff()
    {
        ActiveTimeSlowUser user = (ActiveTimeSlowUser)owner;
        user.isStayKey = true;
    }
}
