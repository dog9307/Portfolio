using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowDamageUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = ((MagicArrowUser)owner);
        user.additionalDamage += 4.0f;
    }

    public override void BuffOff()
    {
        MagicArrowUser user = ((MagicArrowUser)owner);
        user.additionalDamage -= 4.0f;
        if (user.additionalDamage < 0.0f)
            user.additionalDamage = 0.0f;
    }
}
