using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeDebuffAdditive : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.isDebuffAdditive = true;
    }

    public override void BuffOff()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.isDebuffAdditive = false;
    }
}
