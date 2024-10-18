using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCounterAttackHelper : CounterAttackUserHelperBase
{
    public override void Init()
    {
        owner.transform.localPosition = Vector2.zero;
    }

    public override void Release()
    {
    }

    public override bool IsCanCounterAttack()
    {
        _currentTarget = null;

        CanCounterAttackedObject[] objs = GameObject.FindObjectsOfType<CanCounterAttackedObject>();
        float minDistance = maxDistance;
        foreach (var obj in objs)
        {
            //if (obj.gameObject == _prevTarget) continue;
            if (!obj.isCanCountered) continue;
            if (obj.GetComponent<Damagable>().isDie) continue;

            float distance = Vector2.Distance(owner.transform.position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                _currentTarget = obj.gameObject;
                break;
            }
        }

        //if (!_currentTarget && _prevTarget)
        //{
        //    float distance = Vector2.Distance(owner.transform.position, _prevTarget.transform.position);
        //    if (distance < maxDistance)
        //        _currentTarget = _prevTarget;
        //}

        return (_currentTarget != null);
    }

    public override void UseSkill(GameObject newBullet, Vector2 dir)
    {
        CounterAttackDamager damager = newBullet.GetComponentInChildren<CounterAttackDamager>();
        damager.dir = dir;
        damager.target = _currentTarget.GetComponent<Collider2D>();

        float additionalDamage = 0.0f;
        float damageMultiplier = owner.damageMultiplier;
        if (owner.buff)
        {
            additionalDamage += owner.buff.additionalSkillDamage;
            damageMultiplier *= owner.buff.skillDamageMultiplier;
        }
        
        Damage realDamage = damager.damage;
        // Player Buff   + Skill buff
        realDamage.additionalDamage = additionalDamage + owner.additionalDamage;
        realDamage.damageMultiplier = (damageMultiplier + owner.additionalMultiplier);

        damager.damage = realDamage;

        float angle = CommonFuncs.DirToDegree(dir);
        newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));
    }
}
