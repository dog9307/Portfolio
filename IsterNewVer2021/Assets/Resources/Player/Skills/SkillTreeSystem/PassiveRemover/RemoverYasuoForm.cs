using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverYasuoForm : SkillBuffBase
{
    public override void BuffOn()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.effectPrefab = Resources.Load<GameObject>("Player/Bullets/BulletRemover/Prefab/RemoverYasuo");
    }

    public override void BuffOff()
    {
        PassiveRemoverUser user = (PassiveRemoverUser)owner;
        user.effectPrefab = Resources.Load<GameObject>("Player/Bullets/BulletRemover/Prefab/BulletRemover");
    }
}
