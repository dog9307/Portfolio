using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AdvancedCounterAttaclHelper : NormalCounterAttackHelper
{
    public int maxCount { get { return owner.maxCount; } }
    protected int _currentCount;
    public int currentCount { get { return _currentCount; } }

    public bool isFirstStart { get; set; }

    protected GameObject _prevTarget;
    protected List<GameObject> _prevTargets = new List<GameObject>();

    private const float MAX_TIME = 0.3f;
    private float _currentTime;

    public CounterAttackTimer timer { get; set; }

    public bool isLastAttack { get; set; }

    public override void Init()
    {
        base.Init();

        timer = owner.GetComponentInChildren<CounterAttackTimer>();
        SpriteRenderer timerSprite = timer.GetComponent<SpriteRenderer>();
        timerSprite.enabled = false;
    }

    public bool IsFirstCounterAttackTriggered()
    {
        return base.IsCanCounterAttack();
    }

    public abstract bool IsNotFirstCounterAttackTriggered();

    public override bool IsCanCounterAttack()
    {
        if (!isFirstStart)
        {
            _currentCount = maxCount;
            owner.additionalMultiplier = 0.0f;
            if (IsFirstCounterAttackTriggered())
            {
                isFirstStart = true;
                return true;
            }
        }
        else
            return IsNotFirstCounterAttackTriggered();

        return false;
    }

    public void CounterAttackReset()
    {
        isFirstStart = false;
        _prevTargets.Clear();
    }

    public override void UseSkill(GameObject newBullet, Vector2 dir)
    {
        base.UseSkill(newBullet, dir);

        _prevTarget = _currentTarget;
        _prevTargets.Add(_currentTarget);

        _currentCount--;

        if (isLastAttack)
        {
            if (_currentCount <= 0)
            {
                CounterAttackDamager damager = newBullet.GetComponentInChildren<CounterAttackDamager>();

                Damage realDamage = damager.damage;
                realDamage.damage = realDamage.realDamage;
                realDamage.additionalDamage = 0.0f;
                realDamage.damageMultiplier = 2.0f;

                damager.damage = realDamage;
            }
        }
    }
}
