using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldKnockback : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.isEndKnockback = true;
    }

    public override void BuffOff()
    {
        ActiveShieldUser user = (ActiveShieldUser)owner;
        user.isEndKnockback = false;
    }
}
