using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffInfo : MonoBehaviour
{
    // tests
    // ResetBuff 빼고 나머지 함수들 정리좀 하자
    // 지금 있는 함수들 싹 다 없애거나 수정해야할듯
    public float effectSpeedUp { get; set; }
    public float itemSpeedUp { get; set; }
    public float buffSpeedUp { get; set; }
    public float additionalSpeed { get { return (effectSpeedUp + itemSpeedUp + buffSpeedUp); } }
    public float speedMultiplier { get; set; }

    public float additionalDamage { get; set; }
    public float damageMultiplier { get; set; }

    public float additionalSkillDamage { get; set; }
    public float skillDamageMultiplier { get; set; }

    public float criticalPercentage { get; set; }
    public float criticalDamageMultiplier { get; set; }

    public float knockbackIncrease { get; set; }

    public float additionalCrystalGainPer { get; set; }

    // 시야 관련
    public float enemyVisibleRatio { get; set; }
    public float darknessLightMultiplier { get; set; }

    // buff 스킬 관련 bool들
    public bool isTracingArrowOn { get; set; }
    public bool isPassiveReversePowerOn { get; set; }
    public bool isCoolTimePassiveOn { get; set; }
    public bool isRangeAttackOn { get; set; }
    public bool isMarkerOn { get; set; }
    public bool isCanCounterAttack { get; set; }

    // familiar 관련
    public float moveRangeDecrease { get; set; }
    public float familiarDamageDecrease { get; set; }

    void Start()
    {
        ResetBuff();
    }

    public void ResetBuff()
    {
        // 기본 스탯 관련
        effectSpeedUp = 0.0f;
        itemSpeedUp = 0.0f;
        buffSpeedUp = 0.0f;
        speedMultiplier = 1.0f;

        additionalDamage = 0.0f;
        damageMultiplier = 1.0f;

        additionalSkillDamage = 0.0f;
        skillDamageMultiplier = 1.0f;

        criticalPercentage = 0.0f;
        criticalDamageMultiplier = 1.5f;

        knockbackIncrease = 0.0f;

        additionalCrystalGainPer = 0.0f;

        // 시야 관련
        enemyVisibleRatio = 1.0f;
        darknessLightMultiplier = 1.0f;

        // buff 스킬 관련 bool들
        isTracingArrowOn = false;
        isPassiveReversePowerOn = false;
        isCoolTimePassiveOn = false;
        isRangeAttackOn = false;
        isMarkerOn = false;
        isCanCounterAttack = false;

        // familiar 관련 변수들
        moveRangeDecrease = 0.0f;
        familiarDamageDecrease = 0.0f;
    }

    public void AddKnockbackIncrease(float figure)
    {
        knockbackIncrease += figure;
        if (knockbackIncrease < 0.0f)
            knockbackIncrease = 0.0f;
    }

    public void AddFortune(float figure)
    {
        additionalCrystalGainPer += figure;
        if (additionalCrystalGainPer < 0.0f)
            additionalCrystalGainPer = 0.0f;
    }

    public void SpeedBuffOn(float speed)
    {
        buffSpeedUp += speed;
    }

    public void SpeedBuffOff(float speed)
    {
        buffSpeedUp -= speed;
        if (buffSpeedUp < 0.0f)
            buffSpeedUp = 0.0f;
    }

    public void DamageBuffOn(float damage)
    {
        additionalDamage += damage;
    }

    public void DamageBuffOff(float damage)
    {
        additionalDamage -= damage;
        if (additionalDamage < 0.0f)
            additionalDamage = 0.0f;
    }
}
