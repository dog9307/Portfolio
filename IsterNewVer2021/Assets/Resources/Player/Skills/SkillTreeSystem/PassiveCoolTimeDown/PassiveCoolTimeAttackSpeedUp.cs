using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeAttackSpeedUp : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.AttackSpeedUp(true);
    }

    public override void BuffOff()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.AttackSpeedUp(false);
    }
}
