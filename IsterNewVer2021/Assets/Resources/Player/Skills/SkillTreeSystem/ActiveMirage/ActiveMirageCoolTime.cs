using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageCoolTime : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isAllBuff = true;
    }

    public override void BuffOff()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isAllBuff = false;
    }
}
