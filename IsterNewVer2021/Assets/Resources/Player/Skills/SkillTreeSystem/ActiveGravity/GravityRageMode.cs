using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRageMode : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.isRageMode = true;
    }

    public override void BuffOff()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.isRageMode = false;
    }
}
