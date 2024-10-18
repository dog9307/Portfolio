using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalSlow : SkillBuffBase
{
    public override void BuffOn()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isSlow = true;
    }

    public override void BuffOff()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isSlow = false;
    }
}
