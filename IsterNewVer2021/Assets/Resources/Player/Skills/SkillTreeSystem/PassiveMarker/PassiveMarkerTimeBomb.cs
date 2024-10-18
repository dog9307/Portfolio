using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarkerTimeBomb : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isTimeBomb = true;
    }

    public override void BuffOff()
    {
        PassiveMarkerUser user = (PassiveMarkerUser)owner;
        user.isTimeBomb = false;
    }
}
