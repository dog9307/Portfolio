using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackMoreKnockback : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.additionalFigure += 0.1f;
    }

    public override void BuffOff()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.additionalFigure -= 0.1f;
        if (user.additionalFigure < 0.0f)
            user.additionalFigure = 0.0f;
    }
}
