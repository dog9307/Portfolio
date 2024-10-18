using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveKnockbackIncrease : BuffPassive
{
    public override int id { get { return 209; } }
    public override float figure
    {
        get
        {
            float temp = 0.6f;

            //if (_currentLevel == 0)
            //    temp = 0.2f;
            //else if (_currentLevel == 1)
            //    temp = 0.5f;
            //else if (_currentLevel == 2)
            //    temp = 0.7f;

            return temp;
        }
    }

    public override void Init()
    {
        base.Init();

        _cost = 1;

        skillName = "넉백 증가";
        skillDesc = "몬스터가 피격당할 때 넉백되는 정도가 증가한다.";
    }

    public override void BuffOn()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.AddKnockbackIncrease(figure);
    }

    public override void BuffOff()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.AddKnockbackIncrease(-figure);
    }
}
