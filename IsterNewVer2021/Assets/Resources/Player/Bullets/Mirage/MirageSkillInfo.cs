using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirageSkillInfo : SkillInfo
{
    void Start()
    {
        SkillInfo player = FindObjectOfType<PlayerMoveController>().GetComponent<SkillInfo>();
        foreach (var skill in player.passiveSkills)
        {
            if (typeof(PassiveBase).IsInstanceOfType(skill))
            {
                if (typeof(PassiveMirage).IsInstanceOfType(skill) ||
                    typeof(PassiveChargeAttack).IsInstanceOfType(skill))
                {
                    _passiveSkills.Add(null);
                    continue;
                }

                SkillBase newSkill = SkillFactory.CopySkill(skill);
                newSkill.owner = this;
                AddSkill(newSkill);
                //SkillBuffSetting(skill);
            }
            else
                _passiveSkills.Add(null);
        }
    }

    //void SkillBuffSetting(SkillBase origin)
    //{
    //    SkillUserManager originManager = FindObjectOfType<PlayerMoveController>().GetComponentInChildren<SkillUserManager>();
    //    if (!originManager) return;

    //    SkillUserManager mirageManager = GetComponentInChildren<SkillUserManager>();
    //    if (!mirageManager) return;

    //    SkillUser originUser = originManager.FindUser(origin.GetType());
    //    SkillUser mirageUser = mirageManager.FindUser(origin.GetType());
    //    if (!originUser || !mirageUser) return;

    //    mirageUser.Init();
    //    if (typeof(PassiveKnockbackIncreaseUser).IsInstanceOfType(mirageUser))
    //        ((PassiveKnockbackIncreaseUser)mirageUser).KnockbackAssistantTurnOn(((PassiveKnockbackIncreaseUser)originUser).assistant.activeSelf);
    //}
}
