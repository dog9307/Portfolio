using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTimeSlowHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;

             if (buffId == 0) temp = new TimeSlowTimeUp();
        else if (buffId == 1) temp = new TimeSlowSlowUp();
        else if (buffId == 2) temp = new TimeSlowSpeedUp();
        else if (buffId == 3) temp = new TimeSlowBossSlowUp();
        else if (buffId == 4) temp = new TimeSlowTimeUpByKill();
        else if (buffId == 5) temp = new TimeSlowNotStayKey();

        if (temp != null)
        {
            temp.relativeSkillName = "ActiveTimeSlow";
            temp.id = buffId;
        }

        return temp;
    }
}
