using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRangeAttack : FloorActive
{
    public override float damage
    {
        get
        {
            float figure = 10.0f;
            //     if (_currentLevel == 0) figure = 10.0f;
            //else if (_currentLevel == 1) figure = 15.0f;
            //else if (_currentLevel == 2) figure = 20.0f;

            return figure;
        }
    }

    public override float scale
    {
        get
        {
            float figure = 2.5f * 1.5f;
            //     if (_currentLevel == 0) figure *= 1.5f;
            //else if (_currentLevel == 1) figure *= 1.7f;
            //else if (_currentLevel == 2) figure *= 2.3f;

            return figure;
        }
    }

    public override void Init()
    {
        base.Init();

        _type = ACTIVE.RANGE;
        //_swordType = SKILLTYPE.HAMMER;

        skillName = "낙화";
        skillDesc = "발동 버튼을 누르고 있는 동안 마우스 커서를 따라 어디에 낙화를 발동할지 표시해 주고, 발동 버튼을 떼는 순간 해당 위치에 넓은 범위 공격을 합니다.";
    }
}
