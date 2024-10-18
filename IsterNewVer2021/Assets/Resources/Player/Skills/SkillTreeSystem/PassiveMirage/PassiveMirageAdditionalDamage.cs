using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageAdditionalDamage : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.additionalDamage += 4.0f;
        user.damageMultiplier += 0.1f;
    }

    public override void BuffOff()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.additionalDamage -= 4.0f;
        if (user.additionalDamage < 0.0f)
            user.additionalDamage = 0.0f;
        user.damageMultiplier -= 0.1f;
        if (user.damageMultiplier < 0.0f)
            user.damageMultiplier = 0.0f;
    }
}
