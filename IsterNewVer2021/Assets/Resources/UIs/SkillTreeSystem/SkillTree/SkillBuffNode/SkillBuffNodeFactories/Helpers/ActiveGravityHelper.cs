using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGravityHelper :SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new GravityTimeUp();
        else if (buffId == 1) temp = new GravityForceUp();
        else if (buffId == 2) temp = new GravityRangeUp();
        else if (buffId == 3) temp = new GravityEndBomb();
        else if (buffId == 4) temp = new GravityRageMode();
        else if (buffId == 5) temp = new GravityDebuffCountDamaging();
        else if (buffId == 6) temp = new GravityElectricMode();

        if (temp != null)
        {
            temp.relativeSkillName = "ActiveGravity";
            temp.id = buffId;
        }

        return temp;
    }
}
