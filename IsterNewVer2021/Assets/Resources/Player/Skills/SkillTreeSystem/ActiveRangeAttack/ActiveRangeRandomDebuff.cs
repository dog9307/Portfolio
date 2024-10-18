using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRangeRandomDebuff : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.isRandomDebuff = true;
    }

    public override void BuffOff()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.isRandomDebuff = false;
    }
}
