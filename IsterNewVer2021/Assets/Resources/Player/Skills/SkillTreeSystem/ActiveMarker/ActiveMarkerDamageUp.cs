using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarkerDamageUp : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.damageMultiplier = 2.0f;
    }

    public override void BuffOff()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.damageMultiplier = 1.0f;
    }
}
