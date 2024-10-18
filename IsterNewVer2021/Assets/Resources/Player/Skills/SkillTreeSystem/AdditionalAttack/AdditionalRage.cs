using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalRage : SkillBuffBase
{
    public override void BuffOn()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isRage = true;
    }

    public override void BuffOff()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isRage = false;
    }
}
