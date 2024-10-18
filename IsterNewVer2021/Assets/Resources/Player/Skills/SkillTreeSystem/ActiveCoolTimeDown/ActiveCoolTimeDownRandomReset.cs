using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoolTimeDownRandomReset : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveCoolTimeDownUser user = (ActiveCoolTimeDownUser)owner;
        user.isRandomReset = true;
    }

    public override void BuffOff()
    {
        ActiveCoolTimeDownUser user = (ActiveCoolTimeDownUser)owner;
        user.isRandomReset = false;
    }
}
