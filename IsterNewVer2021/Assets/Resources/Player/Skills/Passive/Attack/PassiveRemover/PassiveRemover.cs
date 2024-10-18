using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveRemover : AttackPassive
{
    public override int id { get { return 208; } }
    public override void Init()
    {
        base.Init();
        
        _cost = 2;

        skillName = "탄환지우기";
        skillDesc = "일정 범위 적의 투사체 공격을 지운다.";
    }
}
