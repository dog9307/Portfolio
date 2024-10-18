using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicDashItem : RelicItemBase
{
    public override void Init()
    {
        id = 102;

        GameObject.FindObjectOfType<PlayerSkillUsage>().ActiveDashGain();
    }

    public override void Release()
    {
    }
}
