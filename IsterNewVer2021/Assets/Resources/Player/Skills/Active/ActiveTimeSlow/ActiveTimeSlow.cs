using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveTimeSlow : ActiveBase
{
    public override void Init()
    {
        base.Init();

        _type = ACTIVE.TIMESLOW;
        //_swordType = SKILLTYPE.SARCOPHAGUS;

        skillName = "시간 여행자";
        skillDesc = "발동 버튼을 누르고 있는 동안 해당 룸의 시간이 느려지며 시전자를 제외한 모든 것이 느려집니다.\n일정 시간이 지나면 해제됩니다.";
    }
}
