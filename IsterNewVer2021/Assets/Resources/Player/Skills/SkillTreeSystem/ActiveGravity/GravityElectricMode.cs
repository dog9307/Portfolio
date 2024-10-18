using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityElectricMode : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.isElectricMode = true;
    }

    public override void BuffOff()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.isElectricMode = false;
    }
}
