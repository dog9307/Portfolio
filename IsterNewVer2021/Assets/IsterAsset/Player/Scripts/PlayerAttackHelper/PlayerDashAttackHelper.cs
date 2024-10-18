using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashAttackHelper : PlayerAttackHelperBase
{
    private PlayerMoveController _move;

    public override void Init()
    {
        attacker.isDashAttack = true;
        _move = attacker.GetComponent<PlayerMoveController>();
    }

    public override void Release()
    {
        attacker.isDashAttack = false;
    }

    public override bool IsTriggered()
    {
        if (attacker.equipment)
            attacker.equipment.isBattle = attacker.isBattle;

        if (!IsCanAttack()) return false;

        //PassiveChargeAttackUser charge = skill.FindUser<PassiveChargeAttack>() as PassiveChargeAttackUser;
        if (KeyManager.instance.IsOnceKeyDown("attack_left"))
        {
            //if (attacker.IsCounterTriggered(HAND.LEFT))
            //{
            //    attacker.AttackTriggered(ATTACK_TYPE.COUNTER, HAND.LEFT);

            //    attacker.SkillCancle();

            //    return true;
            //}

            if (!skill.isSkillUsing)
            {
                if (!delay.isLeftDelay)
                {
                    //if (!charge)
                    //{
                    //    attacker.AttackTriggered(ATTACK_TYPE.NORMAL, HAND.LEFT);
                    //    return true;
                    //}
                    //else
                    //{
                    //    if (charge.relativeHand == HAND.LEFT)
                    //        return false;
                    //    else
                    //    {
                            attacker.AttackTriggered(ATTACK_TYPE.NORMAL, HAND.LEFT);
                            return true;
                    //    }
                    //}
                }
            }
        }

        if (KeyManager.instance.IsOnceKeyDown("attack_right"))
        {
            //if (attacker.IsCounterTriggered(HAND.RIGHT))
            //{
            //    attacker.AttackTriggered(ATTACK_TYPE.COUNTER, HAND.RIGHT);

            //    attacker.SkillCancle();

            //    return true;
            //}

            if (!skill.isSkillUsing)
            {
                if (!delay.isRightDelay)
                {
                    //if (!charge)
                    //{
                    //    attacker.AttackTriggered(ATTACK_TYPE.NORMAL, HAND.RIGHT);
                    //    return true;
                    //}
                    //else
                    //{
                    //    if (charge.relativeHand == HAND.RIGHT)
                    //        return false;
                    //    else
                    //    {
                            attacker.AttackTriggered(ATTACK_TYPE.NORMAL, HAND.RIGHT);
                            return true;
                    //    }
                    //}
                }
            }
        }

        return false;
    }

    public override void AttackStart()
    {
        attacker.isAttacking = true;
        _move.dashStartPos = _move.transform.position;

        _move.PlaySFX("dash");

        switch (attacker.attackType)
        {
            case ATTACK_TYPE.NORMAL:
                attacker.NormalAttack();
            break;

            //case ATTACK_TYPE.COUNTER:
            //    attacker.CounterAttack();
            //break;
        }
    }

    public override void AttackEnd()
    {
        attacker.isAttacking = false;

        attacker.AfterImageControl(false);
    }
}
