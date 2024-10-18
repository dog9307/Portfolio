using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackDebuffDamage : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.damageMultiplier = 1.5f;
    }

    public override void BuffOff()
    {
        PassiveKnockbackIncreaseUser user = (PassiveKnockbackIncreaseUser)owner;
        user.damageMultiplier = 1.0f;
    }
}
