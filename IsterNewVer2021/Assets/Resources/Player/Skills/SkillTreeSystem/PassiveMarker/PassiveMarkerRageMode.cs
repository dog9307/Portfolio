using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarkerRageMode : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isRageMode = true;
    }

    public override void BuffOff()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isRageMode = false;
    }
}
