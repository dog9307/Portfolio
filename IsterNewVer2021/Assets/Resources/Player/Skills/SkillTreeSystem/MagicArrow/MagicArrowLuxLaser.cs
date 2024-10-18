using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowLuxLaser : SkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        MagicArrowLuxHelper helper = user.GetHelper<MagicArrowLuxHelper>();

        helper.effectPrefab = Resources.Load<GameObject>("Player/Bullets/MagicArrow/LuxLaser/Prefab/LuxLaser");
        helper.bulletCreatorOffset = 3.0f;
    }

    public override void BuffOff()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        MagicArrowLuxHelper helper = user.GetHelper<MagicArrowLuxHelper>();

        helper.effectPrefab = Resources.Load<GameObject>("Player/Bullets/MagicArrow/NormalMagicArrow/Prefab/MagicArrow");
        helper.bulletCreatorOffset = 1.0f;
    }
}
