using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackRangeIncrease : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        ManyTargetsCounterAttack helper = user.GetHelper<ManyTargetsCounterAttack>();
        helper.isRangeIncrease = true;
    }

    public override void BuffOff()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        ManyTargetsCounterAttack helper = user.GetHelper<ManyTargetsCounterAttack>();
        helper.isRangeIncrease = false;
    }
}
