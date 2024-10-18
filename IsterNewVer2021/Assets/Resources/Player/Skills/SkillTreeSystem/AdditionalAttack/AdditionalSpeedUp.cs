using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalSpeedUp : SkillBuffBase
{
    public override void BuffOn()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isSpeedUp = true;
    }

    public override void BuffOff()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isSpeedUp = false;
    }
}
