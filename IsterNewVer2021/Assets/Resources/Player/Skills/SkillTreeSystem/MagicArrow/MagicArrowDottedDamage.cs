using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowDottedDamage : SkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        PenetrationArrowHelper helper = user.GetHelper<PenetrationArrowHelper>();
        helper.isAddDotted = true;
    }

    public override void BuffOff()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        PenetrationArrowHelper helper = user.GetHelper<PenetrationArrowHelper>();
        helper.isAddDotted = false;
    }
}
