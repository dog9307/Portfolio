using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarkerDecreaseTickTime : StackedSkillBuffBase
{
    public override void BuffOn()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.tickMultiplier += 0.1f;
    }

    public override void BuffOff()
    {
        ActiveMarkerUser user = (ActiveMarkerUser)owner;
        user.tickMultiplier -= 0.1f;
        if (user.tickMultiplier < 0.0f)
            user.tickMultiplier = 0.0f;
    }
}
