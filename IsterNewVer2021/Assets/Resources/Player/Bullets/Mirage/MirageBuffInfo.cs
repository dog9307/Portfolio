using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageBuffInfo : BuffInfo
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerMoveController move = FindObjectOfType<PlayerMoveController>();
        BuffInfo player = move.GetComponent<BuffInfo>();

        effectSpeedUp = player.effectSpeedUp;
        buffSpeedUp = player.buffSpeedUp;
        speedMultiplier = player.speedMultiplier;

        additionalDamage = player.additionalDamage;
        damageMultiplier = player.damageMultiplier;

        additionalSkillDamage = player.additionalSkillDamage;
        skillDamageMultiplier = player.skillDamageMultiplier;
        
        knockbackIncrease = player.knockbackIncrease;

        additionalCrystalGainPer = player.additionalCrystalGainPer;

        isTracingArrowOn = player.isTracingArrowOn;
        isPassiveReversePowerOn = player.isPassiveReversePowerOn;
        isCoolTimePassiveOn = player.isCoolTimePassiveOn;
        isRangeAttackOn = player.isRangeAttackOn;
        isMarkerOn = player.isMarkerOn;
    }
}
