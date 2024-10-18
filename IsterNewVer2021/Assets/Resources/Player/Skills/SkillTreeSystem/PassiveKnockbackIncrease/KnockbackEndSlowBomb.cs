using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackEndSlowBomb : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.isSlowBomb = true;
    }

    public override void BuffOff()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.isSlowBomb = false;
    }
}
