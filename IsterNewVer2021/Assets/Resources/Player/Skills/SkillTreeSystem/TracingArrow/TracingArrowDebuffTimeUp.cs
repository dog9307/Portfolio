using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrowDebuffTimeUp : SkillBuffBase
{
    public override void BuffOn()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.isDebuffTimeUp = true;
    }

    public override void BuffOff()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.isDebuffTimeUp = false;
    }
}
