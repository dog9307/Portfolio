using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackIncreaseHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new KnockbackMoreKnockback();
        else if (buffId == 1) temp = new KnockbackBounceDamage();
        else if (buffId == 2) temp = new KnockbackBounceSlow();
        else if (buffId == 3) temp = new KnockbackDebuffDamage();
        else if (buffId == 4) temp = new KnockbackSlowTimeUp();
        else if (buffId == 5) temp = new KnockbackSecondKnockback();
        else if (buffId == 6) temp = new KnockbackEndSlowBomb();

        if (temp != null)
        {
            temp.relativeSkillName = "PassiveKnockbackIncrease";
            temp.id = buffId;
        }

        return temp;
    }
}
