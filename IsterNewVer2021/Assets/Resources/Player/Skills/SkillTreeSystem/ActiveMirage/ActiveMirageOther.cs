using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageOther : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isOtherScaleUp = true;
    }

    public override void BuffOff()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isOtherScaleUp = false;
    }
}
