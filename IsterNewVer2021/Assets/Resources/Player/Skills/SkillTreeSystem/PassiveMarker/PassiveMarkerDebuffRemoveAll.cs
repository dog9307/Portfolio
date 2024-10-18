using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarkerDebuffRemoveAll : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isRemoveAll = true;
    }

    public override void BuffOff()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isRemoveAll = false;
    }
}
