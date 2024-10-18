using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackBounceDamage : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.bounceDamage += 5.0f;
        user.mode = BOUNCE.DAMAGE;
        user.isBounceDamage = true;
    }

    public override void BuffOff()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.bounceDamage -= 5.0f;
        if (user.bounceDamage <= 0.0f)
        {
            user.bounceDamage = 0.0f;
            user.mode = BOUNCE.NONE;
            user.isBounceDamage = false;
        }
    }
}
