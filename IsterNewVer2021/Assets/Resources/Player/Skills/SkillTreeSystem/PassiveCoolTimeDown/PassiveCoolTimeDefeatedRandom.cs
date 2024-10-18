using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeDefeatedRandom : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.isDefeatCoolTime = true;
    }

    public override void BuffOff()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.isDefeatCoolTime = false;
    }
}
