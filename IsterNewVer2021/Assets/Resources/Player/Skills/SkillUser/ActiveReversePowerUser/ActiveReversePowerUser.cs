using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveReversePowerUser : ActiveUserBase
{
    private PlayerSkillUsage _skill;

    protected override void Start()
    {
        base.Start();

        _skill = transform.parent.GetComponentInParent<PlayerSkillUsage>();
    }

    public override void UseSkill()
    {
        _skill.isActiveReversePowerOn = true;
        GameObject.FindObjectOfType<PlayerEffectManager>().EffectOn("ActiveReversePower");
    }
}
