using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalElectric : SkillBuffBase
{
    public override void BuffOn()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isElectric = true;
    }

    public override void BuffOff()
    {
        AdditionalAttackUser user = (AdditionalAttackUser)owner;
        user.isElectric = false;
    }
}
