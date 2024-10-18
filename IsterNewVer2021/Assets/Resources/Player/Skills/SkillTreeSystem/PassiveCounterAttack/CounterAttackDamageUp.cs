using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackDamageUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        user.additionalDamage += 10.0f;
    }

    public override void BuffOff()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        user.additionalDamage -= 10.0f;
        if (user.additionalDamage < 0.0f)
            user.additionalDamage = 0.0f;
    }
}
