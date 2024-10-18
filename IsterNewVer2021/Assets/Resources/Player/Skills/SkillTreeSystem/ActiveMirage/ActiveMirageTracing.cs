using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageTracing : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isAdditionalTracing = true;
    }

    public override void BuffOff()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isAdditionalTracing = false;
    }
}
