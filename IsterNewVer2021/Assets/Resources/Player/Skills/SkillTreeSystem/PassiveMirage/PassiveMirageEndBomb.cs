using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageEndBomb : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.isEndBomb = true;
    }

    public override void BuffOff()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.isEndBomb = false;
    }
}
