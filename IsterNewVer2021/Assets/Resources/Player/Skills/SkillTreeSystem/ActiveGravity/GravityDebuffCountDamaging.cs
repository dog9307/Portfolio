using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityDebuffCountDamaging : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.isDebuffDamaging = true;
    }

    public override void BuffOff()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.isDebuffDamaging = false;
    }
}
