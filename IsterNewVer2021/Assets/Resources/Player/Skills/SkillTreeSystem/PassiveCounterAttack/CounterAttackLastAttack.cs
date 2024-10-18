using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackLastAttack : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        AdvancedCounterAttaclHelper helper = user.GetHelper<AdvancedCounterAttaclHelper>();
        helper.isLastAttack = true;
    }

    public override void BuffOff()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        AdvancedCounterAttaclHelper helper = user.GetHelper<AdvancedCounterAttaclHelper>();
        helper.isLastAttack = false;
    }
}
