using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveParrying : ActiveBase
{
    public override void Init()
    {
        base.Init();

        _type = ACTIVE.PARRYING;
        //_swordType = SKILLTYPE.SCYTHE;

        skillName = "응수";
        skillDesc = "반격반격ㅎㅎ";
    }
}
