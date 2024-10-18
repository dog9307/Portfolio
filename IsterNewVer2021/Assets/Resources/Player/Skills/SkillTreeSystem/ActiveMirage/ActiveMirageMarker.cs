﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirageMarker : SkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isMarkerDestroy = false;
    }

    public override void BuffOff()
    {
        ActiveMirageUser user = (ActiveMirageUser)owner;
        user.isMarkerDestroy = true;
    }
}
