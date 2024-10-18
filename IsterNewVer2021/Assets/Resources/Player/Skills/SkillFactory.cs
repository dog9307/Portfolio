using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillFactory
{
    public static SkillBase CreateSkill(int id)
    {
        SkillBase skill = null;
        
        // active
             if (id == 100) skill = new ActiveRangeAttack();
        else if (id == 101) skill = new MagicArrow();
        else if (id == 102) skill = new ActiveMirage();
        else if (id == 103) skill = new ActiveDash();
        else if (id == 104) skill = new ActiveReversePower();
        else if (id == 105) skill = new ActiveTimeSlow();
        else if (id == 106) skill = new ActiveMarker();
        else if (id == 107) skill = new ActiveCoolTimeDown();
        else if (id == 108) skill = new ActiveShield();
        else if (id == 109) skill = new ActiveGravity();
        else if (id == 110) skill = new ActiveItemReroll();
        else if (id == 111) skill = new ActiveParrying();
        else if (id == 112) skill = new ActiveYaran();
        else if (id == 113) skill = new ActiveButterfly();

        // passive
        else if (id == 200) skill = new PassiveOtherAttack();
        else if (id == 201) skill = new TracingArrow();
        else if (id == 202) skill = new PassiveMirage();
        else if (id == 203) skill = new AdditionalAttack();
        else if (id == 204) skill = new PassiveReversePower();
        else if (id == 205) skill = new PassiveCounterAttack();
        else if (id == 206) skill = new PassiveMarker();
        else if (id == 207) skill = new PassiveCoolTimeDown();
        else if (id == 208) skill = new PassiveRemover();
        else if (id == 209) skill = new PassiveKnockbackIncrease();
        else if (id == 210) skill = new PassiveFortune();
        else if (id == 211) skill = new PassiveChargeAttack();

        return skill;
    }

    public static SkillBase CopySkill(SkillBase origin)
    {
        SkillBase skill = null;

        skill = CreateSkill(origin.id);

        //for (int i = 0; i < origin.currentLevel; ++i)
        //    skill.Upgrade();

        return skill;
    }
}
