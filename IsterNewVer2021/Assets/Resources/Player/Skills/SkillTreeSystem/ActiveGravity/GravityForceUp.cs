using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityForceUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.additionalForce += 0.5f;
    }

    public override void BuffOff()
    {
        ActiveGravityUser user = (ActiveGravityUser)owner;
        user.additionalForce -= 0.5f;
        if (user.additionalForce < 0.0f)
            user.additionalForce = 0.0f;
    }
}
