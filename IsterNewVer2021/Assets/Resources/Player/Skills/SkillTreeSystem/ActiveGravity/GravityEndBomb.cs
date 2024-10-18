using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEndBomb : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.isEndBomb = true;
    }

    public override void BuffOff()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.isEndBomb = false;
    }
}
