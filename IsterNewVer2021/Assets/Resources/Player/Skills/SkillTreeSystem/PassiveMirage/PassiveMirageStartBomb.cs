using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageStartBomb : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.isStartBomb = true;
    }

    public override void BuffOff()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.isStartBomb = false;
    }
}
