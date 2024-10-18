using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerF1BuffItem : TowerRuleItemBase
{
    protected BuffInfo _buff;
    protected PlayerEffectManager _effect;

    public override void Init()
    {
        _buff = GameObject.FindObjectOfType<PlayerMoveController>().GetComponent<BuffInfo>();
        if (_buff == null) return;

        if (_buff)
            _effect = _buff.GetComponentInChildren<PlayerEffectManager>();

        BuffOn();
    }

    public override void Release()
    {
        if (_buff == null) return;

        BuffOff();
    }

    public abstract void BuffOn();
    public abstract void BuffOff();
}
