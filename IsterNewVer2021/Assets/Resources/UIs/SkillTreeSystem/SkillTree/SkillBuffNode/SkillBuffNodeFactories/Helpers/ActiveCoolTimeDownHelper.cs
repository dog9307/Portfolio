using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoolTimeDownHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;
        
             if (buffId == 0) temp = new ActiveCoolTimeDownFirstSlot();
        else if (buffId == 1) temp = new ActiveCoolTimeDownSecondSlot();
        else if (buffId == 2) temp = new ActiveCoolTimeDownThirdSlot();
        else if (buffId == 3) temp = new ActiveCoolTimeDownForthSlot();
        else if (buffId == 4) temp = new ActiveCoolTimeDownFifthSlot();
        else if (buffId == 5) temp = new ActiveCoolTimeDownSelf();
        else if (buffId == 6) temp = new ActiveCoolTimeDownRandomReset();

        if (temp != null)
        {
            temp.relativeSkillName = "ActiveCoolTimeDown";
            temp.id = buffId;
        }

        return temp;
    }
}
