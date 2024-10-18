using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageSkillUse : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.isSkillUse = true;
    }

    public override void BuffOff()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.isSkillUse = false;
    }
}
