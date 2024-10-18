using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRangeRangeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.scaleMultiplier += 0.2f;
    }

    public override void BuffOff()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.scaleMultiplier -= 0.2f;
        if (user.scaleMultiplier < 1.0f)
            user.scaleMultiplier = 1.0f;
    }
}
