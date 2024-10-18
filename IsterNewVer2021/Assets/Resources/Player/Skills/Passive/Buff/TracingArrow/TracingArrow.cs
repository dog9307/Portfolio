using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracingArrow : BuffPassive
{
    public override int id { get { return 201; } }
    // test
    public float damageMultiplier { get; set; }

    public override void Init()
    {
        base.Init();

        _cost = 1;

        skillName = "추적 마력탄";
        skillDesc = "플레이어의 공격에 적중한 상대들을 추적하는 마력탄을 발사한다.";
    }

    public override void BuffOn()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isTracingArrowOn = true;
    }

    public override void BuffOff()
    {
        BuffInfo buff = owner.GetComponent<BuffInfo>();
        buff.isTracingArrowOn = false;
    }
}
