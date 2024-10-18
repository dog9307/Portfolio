using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCoolTimeDownSelf : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveCoolTimeDownUser user = (ActiveCoolTimeDownUser)owner;
        user.isSelfDown = true;
        user.SetAdditiveFigure(user.itSelfIndex, 3.0f);
    }

    public override void BuffOff()
    {
        ActiveCoolTimeDownUser user = (ActiveCoolTimeDownUser)owner;
        user.SetAdditiveFigure(user.itSelfIndex, -3.0f);
    }
}
