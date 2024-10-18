using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarkerStack : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.isStack = true;
    }

    public override void BuffOff()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.isStack = false;
    }
}
