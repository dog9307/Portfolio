using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossControllerBase : MonoBehaviour
{
    [HideInInspector]
    public BossHpBarSetter _hpBarSetter;

    protected BossBase _baseInfo;

    public abstract void BossWakeUp();
    public virtual void BossAppear() { }
    public abstract void BossDie();
    public virtual void BossDisappear() { }

    public void SetBossBase(BossBase _base)
    {
        _baseInfo = _base;
        if(_hpBarSetter)
        {
            _hpBarSetter._boss = _baseInfo;
        }
    }

    public void SetBossController(BossControllerBase _boss)
    {
        _hpBarSetter = GetComponent<BossHpBarSetter>();
        _hpBarSetter._bossController = _boss;
    }
}
