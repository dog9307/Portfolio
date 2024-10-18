using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackOneTarget : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        user.maxCount += 1;
        if (!user.HelperCheck(typeof(OneTargetCounterAttack)))
            user.ApplyHelper(new OneTargetCounterAttack());
    }

    public override void BuffOff()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        user.maxCount -= 1;
        if (user.maxCount <= 1)
        {
            user.maxCount = 1;
            if (user.HelperCheck(typeof(OneTargetCounterAttack)))
                user.ApplyHelper(new NormalCounterAttackHelper());
        }
    }
}
