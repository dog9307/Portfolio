using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveShieldHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new ShieldTimeUp();
        else if (buffId == 1) temp = new ShieldTimeDown();
        else if (buffId == 2) temp = new ShieldCanMoving();
        else if (buffId == 3) temp = new ShieldCoolTimeDown();
        else if (buffId == 4) temp = new ShieldStayKey();
        else if (buffId == 5) temp = new ShieldKnockback();

        if (temp != null)
        {
            temp.relativeSkillName = "ActiveShield";
            temp.id = buffId;
        }

        return temp;
    }
}
