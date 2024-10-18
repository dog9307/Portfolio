using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackBounceSlow : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.slowFigure = 0.5f;
        user.mode = BOUNCE.DEBUFF;
        user.isBounceDamage = true;
    }

    public override void BuffOff()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.slowFigure = 0.0f;
        user.mode = BOUNCE.NONE;
        user.isBounceDamage = false;
    }
}
