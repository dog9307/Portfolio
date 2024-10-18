using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new CounterAttackDamageUp();
        else if (buffId == 1) temp = new CounterAttackOneTarget();
        else if (buffId == 2) temp = new CounterAttackManyTargets();
        else if (buffId == 3) temp = new CounterAttackDamageIncrease();
        else if (buffId == 4) temp = new CounterAttackRangeIncrease();
        else if (buffId == 5) temp = new CounterAttackLastAttack();

        if (temp != null)
        {
            temp.relativeSkillName = "PassiveCounterAttack";
            temp.id = buffId;
        }

        return temp;
    }
}
