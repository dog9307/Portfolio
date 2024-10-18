using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMirageOneMore : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.isOneMore = true;
    }

    public override void BuffOff()
    {
        PassiveMirageUser user = (PassiveMirageUser)owner;
        user.isOneMore = false;
    }
}
