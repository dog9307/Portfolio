using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherPoison : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.isPoisonFlooring = true;
    }

    public override void BuffOff()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.isPoisonFlooring = false;
    }
}
