using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateResetter : DamagableStateResetter
{
    private PlayerAttacker _attack;
    private PlayerDashDelay _delay;
    private PlayerSkillUsage _skill;
    private SkillUserManager _userManager;

    void Start()
    {
        _attack = GetComponent<PlayerAttacker>();
        _delay = GetComponent<PlayerDashDelay>();
        _skill = GetComponent<PlayerSkillUsage>();
        _userManager = GetComponentInChildren<SkillUserManager>();
    }

    public override void StateReset()
    {
        if (_attack)
            _attack.AttackEnd();
        
        if (_delay)
            _delay.DelayStart();

        if (_skill)
        {
            _skill.isSkillUsing = false;

            PassiveChargeAttackUser charge = _skill.FindUser<PassiveChargeAttack>() as PassiveChargeAttackUser;
            if (charge)
                charge.ChargeCancle(true);
        }

        if (_userManager)
        {
            _userManager.SkillEndAll();

            MagicArrowUser magicArrow = _userManager.FindUser(typeof(MagicArrow)) as MagicArrowUser;
            if (magicArrow)
            {
                ChargingArrowCreator creator = magicArrow.GetComponentInChildren<ChargingArrowCreator>();
                if (creator)
                    Destroy(creator.gameObject);
            }

            PassiveCounterAttackUser counterAttack = _userManager.FindUser(typeof(PassiveCounterAttack)) as PassiveCounterAttackUser;
            if (counterAttack)
                counterAttack.CounterAttackReset();

            ActiveParryingUser parrying = _userManager.FindUser(typeof(ActiveParrying)) as ActiveParryingUser;
            if (parrying)
                parrying.SkillEnd();

            ActiveTimeSlowUser slow = _userManager.FindUser(typeof(ActiveTimeSlow)) as ActiveTimeSlowUser;
            if (slow)
                slow.SkillEnd();
        }
    }
}
