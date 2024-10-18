using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItemReroll : ActiveBase
{
    public override void Init()
    {
        base.Init();

        _type = ACTIVE.REROLL;

        skillName = "아이템 리롤";
        skillDesc = "화면에 보이는 아이템을 바꾼다.";
    }
}
