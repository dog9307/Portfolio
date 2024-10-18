using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArrowAttackHelper : PlayerAttackHelperBase
{
    public override void Init()
    {
        _isCoolTime = false;
    }

    public override bool IsTriggered()
    {
        if (attacker.equipment)
            attacker.equipment.isBattle = attacker.isBattle;

        if (!IsCanAttack()) return false;

        if (KeyManager.instance.IsOnceKeyDown("attack_left"))
        {
            if (!skill.isSkillUsing)
            {
                if (!delay.isLeftDelay)
                {
                    attacker.AttackTriggered(ATTACK_TYPE.NORMAL, HAND.LEFT);
                    return true;
                }
            }
        }

        //if (KeyManager.instance.IsOnceKeyDown("attack_right"))
        //{
        //    //if (attacker.IsCounterTriggered(HAND.RIGHT))
        //    //{
        //    //    attacker.AttackTriggered(ATTACK_TYPE.COUNTER, HAND.RIGHT);

        //    //    attacker.SkillCancle();

        //    //    return true;
        //    //}

        //    if (!skill.isSkillUsing)
        //    {
        //        if (!delay.isRightDelay)
        //        {
        //            //if (!charge)
        //            //{
        //            //    attacker.AttackTriggered(ATTACK_TYPE.NORMAL, HAND.RIGHT);
        //            //    return true;
        //            //}
        //            //else
        //            //{
        //            //    if (charge.relativeHand == HAND.RIGHT)
        //            //        return false;
        //            //    else
        //            //    {
        //            attacker.AttackTriggered(ATTACK_TYPE.NORMAL, HAND.RIGHT);
        //            return true;
        //            //    }
        //            //}
        //        }
        //    }
        //}

        return false;
    }

    public override void Release()
    {
    }

    private float _beforeDelay = 0.15f;
    public override void AttackStart()
    {
        attacker.Appear();
        attacker.StartCoroutine(BeforeDelay());
        attacker.StartCoroutine(CoolTime());
    }

    IEnumerator BeforeDelay()
    {
        yield return new WaitForSeconds(_beforeDelay);

        attacker.CreateBullet();

        if (attacker.equipment)
            attacker.equipment.Attack();
    }

    private float _coolTime = 0.6f;
    private bool _isCoolTime;
    IEnumerator CoolTime()
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(_coolTime);
        _isCoolTime = false;
    }

    public override void AttackEnd()
    {
    }

    protected override bool IsCanAttack()
    {
        return base.IsCanAttack() && !attacker.equipment.isAttacking && !_isCoolTime;
    }
}
