using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeCriticalUp : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
    }

    public override void BuffOff()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
    }
}
