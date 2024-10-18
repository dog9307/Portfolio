using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageCounter : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isForcedCounterAttack = true;
    }

    public override void BuffOff()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isForcedCounterAttack = false;
    }
}
