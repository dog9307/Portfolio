using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveMarker : BuffPassive
{
    public override int id { get { return 206; } }
    public override void Init()
    {
        base.Init();

        _cost = 2;

        skillName = "표식";
        skillDesc = "일반 공격에 맞은 적에 추가 데미지를 주는 표식을 남긴다.";
    }

    public override void BuffOn()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isMarkerOn = true;
    }

    public override void BuffOff()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isMarkerOn = false;
    }
}
