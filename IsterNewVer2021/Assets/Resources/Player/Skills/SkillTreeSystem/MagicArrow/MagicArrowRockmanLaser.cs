using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrowRockmanLaser : SkillBuffBase
{
    public override void BuffOn()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        MagicArrowChargingHelper helper = user.GetHelper<MagicArrowChargingHelper>();

        helper.bulletPrefab = Resources.Load<GameObject>("Player/Bullets/MagicArrow/ChargingArrow/Prefab/ChargingLaser");

        Animator playerAnim = GameObject.FindObjectOfType<PlayerMoveController>().GetComponent<Animator>();
        playerAnim.SetBool("isChargingLaser", true);
    }

    public override void BuffOff()
    {
        MagicArrowUser user = (MagicArrowUser)owner;
        MagicArrowChargingHelper helper = user.GetHelper<MagicArrowChargingHelper>();
        
        helper.bulletPrefab = Resources.Load<GameObject>("Player/Bullets/MagicArrow/NormalMagicArrow/Prefab/MagicArrow");

        Animator playerAnim = GameObject.FindObjectOfType<PlayerMoveController>().GetComponent<Animator>();
        playerAnim.SetBool("isChargingLaser", false);
    }
}
