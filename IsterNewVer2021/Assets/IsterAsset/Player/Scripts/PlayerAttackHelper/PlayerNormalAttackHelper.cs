using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackHelper : PlayerAttackHelperBase
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

        if (KeyManager.instance.IsOnceKeyDown("attack_right"))
        {
            if (!skill.isSkillUsing)
            {
                if (!delay.isRightDelay)
                {
                    attacker.AttackTriggered(ATTACK_TYPE.NORMAL, HAND.RIGHT);
                    return true;
                }
            }
        }

        return false;
    }

    public override void Release()
    {
    }

    private float _beforeDelay = 0.3f;
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

    private float _coolTime = 0.4f;
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
