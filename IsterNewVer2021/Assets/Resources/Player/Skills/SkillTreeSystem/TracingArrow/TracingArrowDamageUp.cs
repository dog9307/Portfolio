using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrowDamageUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.additionalDamage += 10.0f;
    }

    public override void BuffOff()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.additionalDamage -= 10.0f;
        if (user.additionalDamage < 0.0f)
            user.additionalDamage = 0.0f;
    }
}
