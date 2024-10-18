using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarkerTimeUp : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.additionalTime = 2.0f;
    }

    public override void BuffOff()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.additionalTime = 0.0f;
    }
}
