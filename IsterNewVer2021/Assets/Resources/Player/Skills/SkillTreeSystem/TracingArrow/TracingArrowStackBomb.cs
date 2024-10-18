using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrowStackBomb : SkillBuffBase
{
    public override void BuffOn()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.isStackTracing = true;
        user.maxStack = 2;
    }

    public override void BuffOff()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.isStackTracing = false;
        user.maxStack = 0;
    }
}
