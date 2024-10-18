using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveOtherAttackHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new OtherFigureLevelUp();
        else if (buffId == 1) temp = new OtherRangeUp();
        else if (buffId == 2) temp = new OtherBackShootForm();
        else if (buffId == 3) temp = new OtherFlooringForm();
        else if (buffId == 4) temp = new OtherStingForm();
        else if (buffId == 5) temp = new OtherKnockback();
        else if (buffId == 6) temp = new OtherPoison();
        else if (buffId == 7) temp = new OtherDebuffDamaging();
             
        if (temp != null)
        {
            temp.relativeSkillName = "PassiveOtherAttack";
            temp.id = buffId;
        }

        return temp;
    }
}
