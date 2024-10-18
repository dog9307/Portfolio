using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverStackDamaging : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.isStackDamaging = true;
        user.stackCount = 0;
    }

    public override void BuffOff()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.isStackDamaging = false;
        user.stackCount = 0;
    }
}
