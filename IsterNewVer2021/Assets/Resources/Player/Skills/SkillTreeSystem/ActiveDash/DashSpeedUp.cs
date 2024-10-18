using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashSpeedUp : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.isSpeedUp = true;
    }

    public override void BuffOff()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.isSpeedUp = false;
    }
}
