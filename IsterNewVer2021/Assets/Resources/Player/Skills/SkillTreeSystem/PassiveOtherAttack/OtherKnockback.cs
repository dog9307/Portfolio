using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherKnockback : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.isKnockback = true;
    }

    public override void BuffOff()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.isKnockback = false;
    }
}
