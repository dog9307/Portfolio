using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveDash : CountableActive
{
    public override void Init()
    {
        base.Init();

        //otherType = typeof(AdditionalAttack);

        _type = ACTIVE.DASH;
        //_swordType = SKILLTYPE.RAPIER;

        skillName = "실바람";
        skillDesc = "바라보는 방향으로 돌진합니다. 돌진하는 동안 데미지를 입지 않습니다.\n" + "전설적인 세공사가 신비한 광물 '이스터리움'을 깎아 부드러운 바람의 힘을 불어넣은 각인. 사용자는 부드러운 바람을 타고 빠른 속도로 돌진하면서 공격을 회피할 수 있다.";
    }

    public override void UseSkill()
    {
        base.UseSkill();

        owner.GetComponentInChildren<SkillUserManager>().UseSkill(GetType());
    }
}
