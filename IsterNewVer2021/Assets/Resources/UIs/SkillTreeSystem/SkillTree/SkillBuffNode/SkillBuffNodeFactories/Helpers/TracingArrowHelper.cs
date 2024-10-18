using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrowHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new TracingArrowDamageUp();
        else if (buffId == 1) temp = new TracingArrowStackBomb();
        else if (buffId == 2) temp = new TracingArrowDebuffTimeUp();
        else if (buffId == 3) temp = new TracingArrowRandomDebuff();
        else if (buffId == 4) temp = new TracingArrowStackUp();

        if (temp != null)
        {
            temp.relativeSkillName = "TracingArrow";
            temp.id = buffId;
        }

        return temp;
    }
}
