using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMirage : ActiveBase
{
    public float damage
    {
        get { return 8.0f; }
    }

    public float damageMultiplier
    {
        get
        {
            float figure = 0.5f;
            //     if (_currentLevel == 0) figure = 0.5f;
            //else if (_currentLevel == 1) figure = 0.6f;
            //else if (_currentLevel == 2) figure = 0.7f;
            //else if (_currentLevel == 3) figure = 1.0f;

            return figure;
        }
    }

    public override void Init()
    {
        base.Init();

        _type = ACTIVE.MIRAGE;
        //_swordType = SKILLTYPE.RAPIER;

        skillName = "흉내쟁이";
        skillDesc = "기본 공격과 같은 공격을 마우스 방향으로 발사합니다.\n발사한 기본 공격은 적용 가능한 모든 패시브 스킬의 효과를 그대로 적용 받습니다.";
    }
}
