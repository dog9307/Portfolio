using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRangeDebuffDamaging : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.isDebuffDamaging = true;
        user.additionalDamage += 2.0f;
    }

    public override void BuffOff()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.additionalDamage -= 2.0f;
        if (user.additionalDamage < 0.0f)
        {
            user.additionalDamage = 0.0f;
            user.isDebuffDamaging = false;
        }
    }
}
