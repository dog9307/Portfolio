using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverTimeUp : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.timeMultiplier = 1.5f;
    }

    public override void BuffOff()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.timeMultiplier = 1.0f;
    }
}
