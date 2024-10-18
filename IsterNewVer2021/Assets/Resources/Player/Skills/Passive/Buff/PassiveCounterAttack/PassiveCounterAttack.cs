using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCounterAttack : BuffPassive
{
    public override int id { get { return 205; } }

    public override void Init()
    {
        base.Init();

        _cost = 2;

        skillName = "카운터 어택";
        skillDesc = "적의 공격에 반격한다.";
    }

    public override void BuffOn()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isCanCounterAttack = true;
    }

    public override void BuffOff()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isCanCounterAttack = false;
    }
}
