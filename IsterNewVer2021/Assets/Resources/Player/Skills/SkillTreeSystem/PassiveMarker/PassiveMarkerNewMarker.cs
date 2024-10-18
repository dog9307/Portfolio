using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarkerNewMarker : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isNewMarker = true;
    }

    public override void BuffOff()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isNewMarker = false;
    }
}
