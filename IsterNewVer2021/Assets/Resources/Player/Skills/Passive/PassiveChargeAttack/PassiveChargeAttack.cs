using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveChargeAttack : PassiveBase
{
    public override int id { get { return 211; } }
    public int maxCount { get { return 1; } }

    public override void Init()
    {
        base.Init();

        _cost = 4;

        skillName = "기다리는 자의 부적";
        skillDesc = "장착 시 기본 공격을 충전할 수 있습니다.\n기본 공격 버튼을 누르고 있는 동안 전진할 수 있는 거리를 표시해 주며, 늘어나는 거리만큼 공격이 강화되고 마우스 방향으로 강력한 일격을 날리며 전진합니다.";
    }

    //public override void DescSetting()
    //{
    //    skillName = "차지 어택";
    //    skillDesc = "플레이어의 공격이 기를 모아 사용하는것으로 변경된다.";
    //}

    public override void UseSkill()
    {

    }
}
