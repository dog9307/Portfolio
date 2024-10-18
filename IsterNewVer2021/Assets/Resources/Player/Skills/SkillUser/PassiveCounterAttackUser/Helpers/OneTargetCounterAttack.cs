using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTargetCounterAttack : AdvancedCounterAttaclHelper
{
    public bool isDamageIncrease { get; set; }

    public override bool IsNotFirstCounterAttackTriggered()
    {
        if (timer)
        {
            if (!timer.isCanCounter)
                return false;
        }

        if (_currentCount <= 0)
            return false;

        if (_prevTargets.Count <= 0) return false;

        _currentTarget = null;
        
        float minDistance = maxDistance;
        if (!_currentTarget && _prevTarget)
        {
            if (_prevTarget.GetComponent<Damagable>().isDie) return false;

            float distance = Vector2.Distance(owner.transform.position, _prevTargets[0].transform.position);
            if (distance < maxDistance)
                _currentTarget = _prevTarget;
        }
        
        return (_currentTarget != null);
    }

    public override void UseSkill(GameObject newBullet, Vector2 dir)
    {
        base.UseSkill(newBullet, dir);
        
        if (isDamageIncrease)
            owner.additionalMultiplier += 0.2f;
    }
}
