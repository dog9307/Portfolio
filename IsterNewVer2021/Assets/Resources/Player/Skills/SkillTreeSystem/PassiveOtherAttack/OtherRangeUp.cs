using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherRangeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.scaleFactor += 0.2f;
    }

    public override void BuffOff()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.scaleFactor -= 0.2f;
        if (user.scaleFactor < 1.0f)
            user.scaleFactor = 1.0f;
    }
}
