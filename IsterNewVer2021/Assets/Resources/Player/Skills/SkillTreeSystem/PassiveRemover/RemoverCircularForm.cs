using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverCircularForm : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.effectPrefab = Resources.Load<GameObject>("Player/Bullets/BulletRemover/Prefab/RemoverCircular");
    }

    public override void BuffOff()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.effectPrefab = Resources.Load<GameObject>("Player/Bullets/BulletRemover/Prefab/BulletRemover");
    }
}
