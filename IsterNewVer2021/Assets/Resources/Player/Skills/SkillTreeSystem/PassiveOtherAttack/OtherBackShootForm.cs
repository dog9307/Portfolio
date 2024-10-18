using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBackShootForm : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        GameObject prefab = Resources.Load<GameObject>("Player/Bullets/OtherAttack/Prefab/OtherShoot");
        user.ChangePrefab(prefab);
    }

    public override void BuffOff()
    {
        PassiveOtherAttackUser user = (PassiveOtherAttackUser)owner;
        GameObject prefab = Resources.Load<GameObject>("Player/Bullets/OtherAttack/Prefab/OtherAttack");
        user.ChangePrefab(prefab);
    }
}
