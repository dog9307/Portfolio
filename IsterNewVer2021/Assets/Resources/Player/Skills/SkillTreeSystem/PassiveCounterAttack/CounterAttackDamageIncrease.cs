using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackDamageIncrease : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        OneTargetCounterAttack helper = user.GetHelper<OneTargetCounterAttack>();
        helper.isDamageIncrease = true;
    }

    public override void BuffOff()
    {
        PassiveCounterAttackUser user = (PassiveCounterAttackUser)owner;
        OneTargetCounterAttack helper = user.GetHelper<OneTargetCounterAttack>();
        helper.isDamageIncrease = false;
    }
}
