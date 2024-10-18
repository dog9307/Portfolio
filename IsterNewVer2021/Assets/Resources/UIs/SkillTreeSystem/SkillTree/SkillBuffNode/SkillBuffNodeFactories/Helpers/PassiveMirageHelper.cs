using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new PassiveMirageAdditionalDamage();
        else if (buffId == 1) temp = new PassiveMirageStartBomb();
        else if (buffId == 2) temp = new PassiveMirageEndBomb();
        else if (buffId == 3) temp = new PassiveMirageCreateFaster();
        else if (buffId == 4) temp = new PassiveMirageSpeedUp();
        else if (buffId == 5) temp = new PassiveMirageOneMore();
        else if (buffId == 6) temp = new PassiveMirageSkillUse();

        if (temp != null)
        {
            temp.relativeSkillName = "PassiveMirage";
            temp.id = buffId;
        }

        return temp;
    }
}
