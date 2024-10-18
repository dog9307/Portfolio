using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStayKey : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.isStayKey = true;
    }

    public override void BuffOff()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.isStayKey = false;
    }
}
