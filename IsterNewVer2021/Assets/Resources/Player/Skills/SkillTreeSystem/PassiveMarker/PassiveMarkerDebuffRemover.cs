using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarkerDebuffRemover : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isDebuffRemover = true;
    }

    public override void BuffOff()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isDebuffRemover = false;
    }
}
