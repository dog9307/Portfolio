using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoolTimeDownSecondSlot : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveCoolTimeDownUser user = (ActiveCoolTimeDownUser)owner;
        user.SetAdditiveFigure(1, 3.0f);
    }

    public override void BuffOff()
    {
        ActiveCoolTimeDownUser user = (ActiveCoolTimeDownUser)owner;
        user.SetAdditiveFigure(1, -3.0f);
    }
}
