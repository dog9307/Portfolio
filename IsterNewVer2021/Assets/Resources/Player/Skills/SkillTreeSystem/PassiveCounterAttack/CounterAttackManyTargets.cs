using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackManyTargets : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        user.maxCount += 1;
        if (!user.HelperCheck(typeof(ManyTargetsCounterAttack)))
            user.ApplyHelper(new ManyTargetsCounterAttack());
    }

    public override void BuffOff()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        user.maxCount -= 1;
        if (user.maxCount <= 1)
        {
            user.maxCount = 1;
            if (user.HelperCheck(typeof(ManyTargetsCounterAttack)))
                user.ApplyHelper(new NormalCounterAttackHelper());
        }
    }
}
