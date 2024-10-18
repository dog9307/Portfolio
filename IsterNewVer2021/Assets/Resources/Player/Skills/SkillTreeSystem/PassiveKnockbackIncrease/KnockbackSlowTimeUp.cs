using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackSlowTimeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.additionalSlowTime += 1.0f;
    }

    public override void BuffOff()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.additionalSlowTime -= 1.0f;
        if (user.additionalSlowTime < 0.0f)
            user.additionalSlowTime = 0.0f;
    }
}
