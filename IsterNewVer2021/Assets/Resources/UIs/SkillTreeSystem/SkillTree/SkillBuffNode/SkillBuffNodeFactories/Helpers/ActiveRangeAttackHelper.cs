using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRangeAttackHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new ActiveRangeRangeUp();
        else if (buffId == 1) temp = new ActiveRangeDebuffDamaging();
        else if (buffId == 2) temp = new ActiveRangeDynamite();
        else if (buffId == 3) temp = new ActiveRangeDebuffTimeUp();
        else if (buffId == 4) temp = new ActiveRangeRandomDebuff();

        if (temp != null)
        {
            temp.relativeSkillName = "ActiveRangeAttack";
            temp.id = buffId;
        }

        return temp;
    }
}
