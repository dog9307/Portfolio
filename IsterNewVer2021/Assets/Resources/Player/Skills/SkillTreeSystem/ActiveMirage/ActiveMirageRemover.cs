using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageRemover : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isMirageDoubleRemover = true;
    }

    public override void BuffOff()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isMirageDoubleRemover = false;
    }
}
