using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveSpeedUp : AttackPassive
{
    public override int id { get { return 205; } }
    public float totalTime { get; set; }
    public float figure { get; set; }

    public override void Init()
    {
        base.Init();

        //otherType = typeof(ActiveTimeSlow);
        
        skillName = "사건의 소실점";
        skillDesc = "공격이 끝난 후 플레이어의 이동속도가 일정 시간 증가한다.";

        // test
        totalTime = 1.0f;
        figure = 5.0f;
    }
}
