using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarkerRandomDebuff : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.isRandomDebuff = true;
    }

    public override void BuffOff()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.isRandomDebuff = false;
    }
}
