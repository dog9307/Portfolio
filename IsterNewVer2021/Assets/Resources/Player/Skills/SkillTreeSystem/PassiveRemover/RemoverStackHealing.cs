using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverStackHealing : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.isStackHealing = true;
        user.stackCount = 0;
    }

    public override void BuffOff()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.isStackHealing = false;
        user.stackCount = 0;
    }
}
