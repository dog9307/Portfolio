using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherFigureLevelUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        ++user.level;
    }

    public override void BuffOff()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        --user.level;
        if (user.level < 0)
            user.level = 0;
    }
}
