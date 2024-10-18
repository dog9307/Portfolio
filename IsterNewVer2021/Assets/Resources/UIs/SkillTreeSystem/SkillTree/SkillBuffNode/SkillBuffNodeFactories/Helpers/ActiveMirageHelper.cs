using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0)  temp = new ActiveMirageOther();
        else if (buffId == 1)  temp = new ActiveMirageTracing();

        else if (buffId == 3)  temp = new ActiveMirageAdditional();

        else if (buffId == 5)  temp = new ActiveMirageCounter();
        else if (buffId == 6)  temp = new ActiveMirageMarker();
        else if (buffId == 7)  temp = new ActiveMirageCoolTime();
        else if (buffId == 8)  temp = new ActiveMirageRemover();
        else if (buffId == 9)  temp = new ActiveMirageKnockback();
        else if (buffId == 10) temp = new ActiveMirageFortune();

        if (temp != null)
        {
            temp.relativeSkillName = "ActiveMirage";
            temp.id = buffId;
        }

        return temp;
    }
}
