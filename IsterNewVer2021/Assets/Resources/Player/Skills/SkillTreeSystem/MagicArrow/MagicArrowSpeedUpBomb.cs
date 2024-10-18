using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowSpeedUpBomb : SkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = ((MagicArrowUser)owner);
        user.ApplyHelper(new MagicArrowBombHelper());
    }

    public override void BuffOff()
    {
        MagicArrowUser user = ((MagicArrowUser)owner);
        user.ApplyHelper(new NormalMagicArrowHelper());
    }
}
