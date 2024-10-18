using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpeedUpUser : SkillUser
{
    public override void UseSkill()
    {
        PlayerMoveController player = GameObject.FindObjectOfType<PlayerMoveController>();
        PlayerEffectManager em = player.GetComponentInChildren<PlayerEffectManager>();
        GameObject effect = em.FindEffect("PassiveSpeedUp");
        if (effect)
        {
            SkillInfo info = player.GetComponent<SkillInfo>();
            PassiveSpeedUp skill = info.FindSkill<PassiveSpeedUp>();

            SpeedUpEffect speedUp = effect.GetComponent<SpeedUpEffect>();
            if (!speedUp) return;

            speedUp.totalTime = skill.totalTime;
            speedUp.figure = skill.figure;

            if (effect.activeSelf)
                speedUp.BuffOn();
            else
                effect.SetActive(true);
        }
    }
}
