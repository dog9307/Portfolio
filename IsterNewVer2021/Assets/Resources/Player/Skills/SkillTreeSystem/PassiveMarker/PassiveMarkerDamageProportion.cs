using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarkerDamageProportion : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isDamageProportion = true;
    }

    public override void BuffOff()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isDamageProportion = false;
    }
}
