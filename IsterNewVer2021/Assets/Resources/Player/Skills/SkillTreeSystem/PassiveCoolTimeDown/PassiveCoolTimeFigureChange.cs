using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeFigureChange : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.isCoolTimeMode = true;
    }

    public override void BuffOff()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.isCoolTimeMode = false;
    }
}
