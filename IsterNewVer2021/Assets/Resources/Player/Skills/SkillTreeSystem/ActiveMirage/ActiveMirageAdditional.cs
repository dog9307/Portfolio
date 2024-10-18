using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageAdditional : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isAdditionalMirage = true;
    }

    public override void BuffOff()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isAdditionalMirage = false;
    }
}
