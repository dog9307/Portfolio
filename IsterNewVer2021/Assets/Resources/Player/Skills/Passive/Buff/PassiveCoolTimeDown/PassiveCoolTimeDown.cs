using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveCoolTimeDown : BuffPassive
{
    public override int id { get { return 207; } }
    public override float figure
    {
        get
        {
            float temp = 0.02f;

            //if (_currentLevel == 0)
            //    temp = 0.02f;
            //else if (_currentLevel == 1)
            //    temp = 0.05f;
            //else if (_currentLevel == 2)
            //    temp = 0.1f;

            return temp;
        }
    }

    public override void Init()
    {
        base.Init();

        //otherType = typeof(ActiveCoolTimeDown);

        skillName = "쿨타임 감소(P)";
        skillDesc = "일반 공격 적중 시 남은 쿨타임이 %로 감소한다.";
    }

    public override void BuffOn()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isCoolTimePassiveOn = true;
    }

    public override void BuffOff()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isCoolTimePassiveOn = false;
    }
}
