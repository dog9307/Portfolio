using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPassive : PassiveBase
{
    public override void UseSkill()
    {
        owner.GetComponentInChildren<SkillUserManager>().UseSkill(GetType());

        //if (!soundName.Equals(""))
        //    SoundManager.instance.PlaySoundEffect(soundName);
    }
}
