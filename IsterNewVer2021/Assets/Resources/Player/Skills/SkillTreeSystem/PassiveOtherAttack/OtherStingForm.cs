using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherStingForm : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.isStingForm = true;
    }

    public override void BuffOff()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        user.isStingForm = false;
    }
}
