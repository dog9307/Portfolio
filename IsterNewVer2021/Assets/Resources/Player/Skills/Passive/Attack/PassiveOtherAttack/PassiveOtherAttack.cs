using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveOtherAttack : AttackPassive
{
    public override int id { get { return 200; } }

    public override void Init()
    {
        base.Init();

        _cost = 1;

        skillName = "추가 공격";
        skillDesc = "플레이어 공격과 다른 추가 공격을 가한다.";

        soundName = "Skill_flower";
    }
}
