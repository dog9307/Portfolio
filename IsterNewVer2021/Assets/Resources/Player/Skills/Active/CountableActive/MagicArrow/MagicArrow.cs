using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicArrow : CountableActive
{
    public override void Init()
    {
        base.Init();

        _type = ACTIVE.MAGICARROW;
        //_swordType = SKILLTYPE.RAPIER;

        skillName = "칼바람";
        skillDesc = "원거리 공격을 날립니다.\n" + "전설적인 세공사가 신비한 광물 '이스터리움'을 깎아 날카로운 바람의 힘을 불어넣은 각인. 사용자는 세찬 바람을 붙잡고 날려 멀리 있는 적을 공격할 수 있다.";
        
        // test
        soundName = "ShotBullet";
    }
}
