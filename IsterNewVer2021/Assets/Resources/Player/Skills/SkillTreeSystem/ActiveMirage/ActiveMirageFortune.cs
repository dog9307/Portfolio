using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageFortune : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
    }

    public override void BuffOff()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
    }
}
