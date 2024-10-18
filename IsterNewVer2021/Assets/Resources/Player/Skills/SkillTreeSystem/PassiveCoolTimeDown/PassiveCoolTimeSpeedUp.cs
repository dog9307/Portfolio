using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeSpeedUp : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.MoveSpeedUp(true);
    }

    public override void BuffOff()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.MoveSpeedUp(false);
    }
}
