using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalWeakness : SkillBuffBase
{
    public override void BuffOn()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isWeakness = true;
    }

    public override void BuffOff()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isWeakness = false;
    }
}
