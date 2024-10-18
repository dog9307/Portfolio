using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverDoubleShoot : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.isDoubleShoot = true;
    }

    public override void BuffOff()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.isDoubleShoot = false;
    }
}
