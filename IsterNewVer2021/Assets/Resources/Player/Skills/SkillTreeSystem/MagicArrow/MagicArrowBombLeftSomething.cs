using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowBombLeftSomething : SkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        MagicArrowBombHelper helper = user.GetHelper<MagicArrowBombHelper>();
        helper.isLavaCreate = true;
    }

    public override void BuffOff()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        MagicArrowBombHelper helper = user.GetHelper<MagicArrowBombHelper>();
        helper.isLavaCreate = false;
    }
}
