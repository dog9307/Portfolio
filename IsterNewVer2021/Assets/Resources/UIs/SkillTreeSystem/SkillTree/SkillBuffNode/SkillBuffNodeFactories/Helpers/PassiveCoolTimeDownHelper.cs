using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeDownHelper : SkillBuffNodeFactoryHelperBase
{
    public override SkillBuffBase CreateBuff(int buffId)
    {
        SkillBuffBase temp = null;
        
             if (buffId == 0) temp = new PassiveCoolTimeAttackSpeedUp();
        else if (buffId == 1) temp = new PassiveCoolTimeFigureLevelUp();
        else if (buffId == 2) temp = new PassiveCoolTimeSpeedUp();
        else if (buffId == 3) temp = new PassiveCoolTimeDefeatedRandom();
        else if (buffId == 4) temp = new PassiveCoolTimeDebuffAdditive();
        else if (buffId == 5) temp = new PassiveCoolTimeCriticalUp();
        else if (buffId == 6) temp = new PassiveCoolTimeFigureChange();
        else if (buffId == 7) temp = new PassiveCoolTimeRandomCoolTimeReset();

        if (temp != null)
        {
            temp.relativeSkillName = "PassiveCoolTimeDown";
            temp.id = buffId;
        }

        return temp;
    }
}
