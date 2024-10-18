using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalAttackHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new AdditionalCountUp();
        else if (buffId == 1) temp = new AdditionalSlow();
        else if (buffId == 2) temp = new AdditionalPoison();
        else if (buffId == 3) temp = new AdditionalElectric();
        else if (buffId == 4) temp = new AdditionalWeakness();
        else if (buffId == 5) temp = new AdditionalRage();
        else if (buffId == 6) temp = new AdditionalSpeedUp();

        if (temp != null)
        {
            temp.relativeSkillName = "AdditionalAttack";
            temp.id = buffId;
        }

        return temp;
    }
}
