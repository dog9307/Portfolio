using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackSecondKnockback : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.isSecondKnockback = true;
    }

    public override void BuffOff()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.isSecondKnockback = false;
    }
}
