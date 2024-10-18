using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRemoverHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new RemoverShootForm();
        else if (buffId == 1) temp = new RemoverYasuoForm();
        else if (buffId == 2) temp = new RemoverCircularForm();
        else if (buffId == 3) temp = new RemoverRangeUp();
        else if (buffId == 4) temp = new RemoverStackDamaging();
        else if (buffId == 5) temp = new RemoverStackHealing();
        else if (buffId == 6) temp = new RemoverTimeUp();
        else if (buffId == 7) temp = new RemoverDoubleShoot();

        if (temp != null)
        {
            temp.relativeSkillName = "PassiveRemover";
            temp.id = buffId;
        }
        
        return temp;
    }
}
