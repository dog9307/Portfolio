using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveGravity : FloorActive
{
    public override float scale { get { return 5.0f; } }
    
    public override void Init()
    {
        base.Init();

        _type = ACTIVE.GRAVITY;
        //_swordType = SKILLTYPE.HAMMER;

        skillName = "중력 폭풍";
        skillDesc = "발동 버튼을 누르고 있는 동안 마우스 커서를 따라 어디에 중력 폭풍을 일으킬지 표시해 주고, 발동 버튼을 떼는 순간 해당 위치에 중력 폭풍을 일으켜 폭풍의 중앙으로 적을 끌어 당깁니다.";
    }
}
