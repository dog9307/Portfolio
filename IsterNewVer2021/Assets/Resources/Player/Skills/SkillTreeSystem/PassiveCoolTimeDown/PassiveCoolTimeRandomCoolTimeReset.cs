using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeRandomCoolTimeReset : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.isRandomReset = true;
    }

    public override void BuffOff()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.isRandomReset = false;
    }
}
