using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDashHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new DashFastStart();
        else if (buffId == 1) temp = new DashCoolTimeDown();
        else if (buffId == 2) temp = new DashDumy();
        else if (buffId == 3) temp = new DashSpeedUp();
        else if (buffId == 4) temp = new DashDumyRangeUp();
        else if (buffId == 5) temp = new DashDumyTimeUp();
        else if (buffId == 6) temp = new DashDumyHPUp();
        else if (buffId == 7) temp = new DashCountUp();

        if (temp != null)
        {
            temp.relativeSkillName = "ActiveDash";
            temp.id = buffId;
        }

        return temp;
    }
}
