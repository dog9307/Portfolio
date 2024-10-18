using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherDebuffDamaging : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.isDebuffDamaging = true;
    }

    public override void BuffOff()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.isDebuffDamaging = false;
    }
}
