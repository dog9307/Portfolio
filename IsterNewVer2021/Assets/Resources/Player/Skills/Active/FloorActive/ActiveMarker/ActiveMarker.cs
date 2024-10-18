using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMarker : FloorActive
{
    public float figure
    {
        get
        {
            float temp = 0.2f;

            //if (_currentLevel == 0)
            //    temp = 0.2f;
            //else if (_currentLevel == 1)
            //    temp = 0.5f;
            //else if (_currentLevel == 2)
            //    temp = 0.7f;

            return temp;
        }
    }

    public override float scale { get { return 5.0f; } }

    public override void Init()
    {
        base.Init();

        _type = ACTIVE.MARKER;
        //_swordType = SKILLTYPE.HAMMER;

        skillName = "살해 표식";
        skillDesc = "발동 버튼을 누르고 있는 동안 마우스 커서를 따라 어디에 살해 표식을 발동할지 표시해 주고, 발동 버튼을 떼는 순간 해당 위치에 파장을 일으켜 범위 안에 있는 적에게 표식을 남깁니다.\n표식이 남겨진 적을 공격하면 추가 데미지를 줍니다.";
    }
}
