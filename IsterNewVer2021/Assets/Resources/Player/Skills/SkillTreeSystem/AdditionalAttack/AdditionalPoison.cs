using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalPoison : SkillBuffBase
{
    public override void BuffOn()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isPoison = true;
    }

    public override void BuffOff()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isPoison = false;
    }
}
