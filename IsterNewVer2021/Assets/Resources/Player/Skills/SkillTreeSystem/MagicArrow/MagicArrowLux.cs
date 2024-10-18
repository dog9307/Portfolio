using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowLux : SkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        user.ApplyHelper(new MagicArrowLuxHelper());
    }

    public override void BuffOff()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        user.ApplyHelper(new NormalMagicArrowHelper());
    }
}
