using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDumy : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.isDumyCreate = true;
    }

    public override void BuffOff()
    {
        ActiveDashUser user = (ActiveDashUser)owner;
        user.isDumyCreate = false;
    }
}
