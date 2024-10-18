using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverRangeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.scaleFactor += 0.25f;
    }

    public override void BuffOff()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.scaleFactor -= 0.25f;
        if (user.scaleFactor < 1.0f)
            user.scaleFactor = 1.0f;
    }
}
