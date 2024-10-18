using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrowStackUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.maxStack += 1;
    }

    public override void BuffOff()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.maxStack -= 1;
        if (user.maxStack < 2)
            user.maxStack = 2;
    }
}
