using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoolTimeDownForthSlot : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveCoolTimeDownUser user = (ActiveCoolTimeDownUser)owner;
        user.SetAdditiveFigure(3, 3.0f);
    }

    public override void BuffOff()
    {
        ActiveCoolTimeDownUser user = (ActiveCoolTimeDownUser)owner;
        user.SetAdditiveFigure(3, -3.0f);
    }
}
