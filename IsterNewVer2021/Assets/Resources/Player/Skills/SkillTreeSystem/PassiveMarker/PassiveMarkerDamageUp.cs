using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarkerDamageUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.additionalDamage += 5.0f;
    }

    public override void BuffOff()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.additionalDamage -= 5.0f;
        if (user.additionalDamage < 0.0f)
            user.additionalDamage = 0.0f;
    }
}
