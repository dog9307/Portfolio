using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageKnockback : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.KnockbackAssistantTurnOn(true);
    }

    public override void BuffOff()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.KnockbackAssistantTurnOn(false);
    }
}
