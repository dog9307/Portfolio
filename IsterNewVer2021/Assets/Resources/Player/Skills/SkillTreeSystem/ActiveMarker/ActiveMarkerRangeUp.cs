using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarkerRangeUp : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.scaleFactor = 2.0f;
    }

    public override void BuffOff()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.scaleFactor = 1.0f;
    }
}
