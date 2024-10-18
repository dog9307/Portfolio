using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveShield : ActiveBase
{
    public override void Init()
    {
        base.Init();

        _type = ACTIVE.SHIELD;
        //_swordType = SKILLTYPE.SARCOPHAGUS;

        skillName = "사건의 지평선";
        skillDesc = "사실 존야";
    }
}
