using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRangeDebuffTimeUp : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.isDebuffTimeUp = true;
    }

    public override void BuffOff()
    {
        ActiveRangeAttackUser user = (ActiveRangeAttackUser)owner;
        user.isDebuffTimeUp = false;
    }
}
