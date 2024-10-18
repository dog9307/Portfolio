using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageCreateFaster : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.createTimeDecrease = 0.2f;
    }

    public override void BuffOff()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.createTimeDecrease = 0.0f;
    }
}
