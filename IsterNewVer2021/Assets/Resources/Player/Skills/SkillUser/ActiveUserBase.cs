using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveUserBase : SkillUser, ICoolTime
{
    [SerializeField]
    protected float _totalCoolTime;
    protected float _currentCoolTime;

    public virtual float currentCoolTime { get { return _currentCoolTime; } set { _currentCoolTime = value; } }
    public virtual float totalCoolTime
    {
        get
        {
            //if (_debuff)
            //    return _totalCoolTime * _debuff.coolTimeMultiplier;
            return _totalCoolTime;
        }
        set { _totalCoolTime = value; }
    }

    public virtual bool isCanUseSkill { get { return !IsCoolTime(); } }

    protected DebuffInfo _debuff;

    protected override void Start()
    {
        base.Start();

        currentCoolTime = totalCoolTime;

        _debuff = _info.GetComponent<DebuffInfo>();
    }

    public virtual void CoolTimeStart()
    {
        currentCoolTime = 0.0f;
        StartCoroutine(CoolTimeUpdate());
    }

    public virtual IEnumerator CoolTimeUpdate()
    {
        while (currentCoolTime < totalCoolTime)
        {
            currentCoolTime += IsterTimeManager.deltaTime / _debuff.coolTimeMultiplier;
            yield return null;
        }
        currentCoolTime = totalCoolTime;
    }
    
    public virtual void CoolTimeDown(float time)
    {
        currentCoolTime += time;
    }

    public virtual bool IsCoolTime()
    {
        return (currentCoolTime < totalCoolTime);
    }
}
