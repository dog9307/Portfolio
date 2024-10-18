using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalAttack : PassiveBase
{
    public override int id { get { return 203; } }
    public int maxCount { get { return 1; } }

    public override void Init()
    {
        base.Init();

        _cost = 3;
        
        skillName = "파란 춤을 추는 자의 부적";
        skillDesc = "장착 시 기본 공격 후의 딜레이 모션을 무시하고 기본 공격을 한 번 더 할 수 있습니다.";
    }

    public override void UseSkill()
    {

    }
}
