using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveFortune : BuffPassive
{
    public override int id { get { return 210; } }
    public override float figure
    {
        get
        {
            float temp = 0.29f;

            //if (_currentLevel == 0)
            //    temp = 0.29f;
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

        skillName = "재화획득+";
        skillDesc = "모든 재화 획득량이 늘어난다.";
    }

    //public override bool Upgrade()
    //{
    //    float temp = figure;
    //    bool isSuccecss = base.Upgrade();
    //    if (isSuccecss)
    //    {
    //        BuffInfo player = GameObject.FindObjectOfType<BuffInfo>();
    //        player.additionalCrystalGainPer -= temp;
    //        player.additionalCrystalGainPer += figure;
    //    }

    //    return isSuccecss;
    //}

    public override void BuffOn()
    {
        // test
        // 단순히 이렇게 해두면 스킬 강화시 문제가 될 수 있음
        // 고로 나중에 바꿔라
        //BuffInfo buff = owner.GetComponent<BuffInfo>();
        //if (typeof(MirageBuffInfo).IsInstanceOfType(buff)) return;

        //buff.AddFortune(figure);
    }

    public override void BuffOff()
    {
        //BuffInfo buff = owner.GetComponent<BuffInfo>();
        //if (typeof(MirageBuffInfo).IsInstanceOfType(buff)) return;

        //buff.AddFortune(-figure);
    }
}
