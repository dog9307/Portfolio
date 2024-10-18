using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveReversePowerUser : SkillUser
{
    public bool isPassiveReversePowerOn { get; set; }
    //public class ReversePowerUserHandChanger : SkillUserHandChanger<PassiveReversePower>
    //{
    //    public override void BuffOn()
    //    {
    //        _buff.isPassiveReversePowerOn = true;
    //    }

    //    public override void BuffOff()
    //    {
    //        _buff.isPassiveReversePowerOn = false;
    //    }
    //}

    public override void UseSkill()
    {
        PlayerSkillUsage skill = FindObjectOfType<PlayerSkillUsage>();
        if (skill)
        {
            skill.isPassiveReversePowerOn = isPassiveReversePowerOn;

            if (skill.isPassiveReversePowerOn)
                GameObject.FindObjectOfType<PlayerEffectManager>().EffectOn("PassiveReversePower");
            else
                GameObject.FindObjectOfType<PlayerEffectManager>().EffectOff("PassiveReversePower");
        }
    }
}
