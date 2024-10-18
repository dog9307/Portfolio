using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManyTargetsCounterAttack : AdvancedCounterAttaclHelper
{
    public bool isRangeIncrease { get; set; }

    public override bool IsNotFirstCounterAttackTriggered()
    {
        if (timer)
        {
            if (!timer.isCanCounter)
                return false;
        }

        if (_currentCount <= 0)
            return false;

        _currentTarget = null;
        
        EnemyBase[] objs = GameObject.FindObjectsOfType<EnemyBase>();
        float minDistance = maxDistance;
        foreach (var obj in objs)
        {
            if (obj.gameObject == _prevTarget) continue;
            if (_prevTargets.Contains(obj.gameObject)) continue;
            if (obj.GetComponent<Damagable>().isDie) continue;

            float distance = Vector2.Distance(owner.transform.position, obj.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                _currentTarget = obj.gameObject;
                break;
            }
        }

        if (_currentTarget == null)
        {
            _prevTargets.Clear();
            minDistance = maxDistance;
            foreach (var obj in objs)
            {
                if (obj.gameObject == _prevTarget) continue;
                if (obj.GetComponent<Damagable>().isDie) continue;

                float distance = Vector2.Distance(owner.transform.position, obj.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    _currentTarget = obj.gameObject;
                    break;
                }
            }
        }

        return (_currentTarget != null);
    }

    public override void UseSkill(GameObject newBullet, Vector2 dir)
    {
        base.UseSkill(newBullet, dir);

        if (isRangeIncrease)
            owner.additionalRange += 2.0f;
    }
}
