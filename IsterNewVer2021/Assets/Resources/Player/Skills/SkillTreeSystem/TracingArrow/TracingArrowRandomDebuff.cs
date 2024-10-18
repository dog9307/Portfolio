using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrowRandomDebuff : SkillBuffBase
{
    public override void BuffOn()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.isRandomDebuff = true;
    }

    public override void BuffOff()
    {
        TracingArrowUser user = (TracingArrowUser)owner;
        user.isRandomDebuff = false;
    }
}
