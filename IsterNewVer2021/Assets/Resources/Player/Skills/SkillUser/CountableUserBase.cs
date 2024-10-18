using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CountableUserBase : ActiveUserBase
{
    [SerializeField]
    private int _defaultCount;

    public int additiveCount { get; set; }
    public virtual int maxCount { get { return (_defaultCount + additiveCount); } }

    protected int _currentCount;
    public int currentCount { get { return _currentCount; } set { _currentCount = value; } }

    public override bool isCanUseSkill { get { return (_currentCount > 0); } }

    protected override void Start()
    {
        base.Start();

        currentCount = maxCount;
    }

    public override void CoolTimeStart()
    {
        if (_currentCount >= maxCount)
        {
            currentCoolTime = 0.0f;
            StartCoroutine(CoolTimeUpdate());
        }
        --_currentCount;
    }

    public override IEnumerator CoolTimeUpdate()
    {
        while (currentCoolTime < totalCoolTime)
        {
            currentCoolTime += IsterTimeManager.deltaTime;
            yield return null;

            if (currentCoolTime >= totalCoolTime)
            {
                ++currentCount;

                if (currentCount < maxCount)
                    currentCoolTime -= totalCoolTime;
            }
        }

        currentCoolTime = totalCoolTime;
    }

    public override void CoolTimeDown(float time)
    {
        currentCoolTime += time;
        while (currentCoolTime >= totalCoolTime)
        {
            currentCount++;
            if (currentCount >= maxCount)
            {
                currentCoolTime = totalCoolTime;
                currentCount = maxCount;
                break;
            }

            currentCoolTime -= totalCoolTime;
        }
    }

    public override bool IsCoolTime()
    {
        if (currentCount >= maxCount)
            currentCoolTime = totalCoolTime;

        return (currentCoolTime < totalCoolTime);
    }
}
