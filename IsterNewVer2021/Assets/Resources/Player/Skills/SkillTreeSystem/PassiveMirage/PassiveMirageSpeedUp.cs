using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageSpeedUp : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.mirageSpeedIncrease = 5.0f;
    }

    public override void BuffOff()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.mirageSpeedIncrease = 0.0f;
    }
}
