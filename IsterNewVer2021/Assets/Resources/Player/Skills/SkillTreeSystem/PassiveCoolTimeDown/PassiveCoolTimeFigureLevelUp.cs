using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeFigureLevelUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.figureLevel += 1;
    }

    public override void BuffOff()
    {
        PassiveCoolTimeDownUser user = (PassiveCoolTimeDownUser)owner;
        user.figureLevel -= 1;
        if (user.figureLevel < 0)
            user.figureLevel = 0;
    }
}
