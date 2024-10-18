using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterTowerToWorld : ColliderTriggerCondition
{
    public override bool IsCanTrigger()
    {
        int count = PlayerPrefs.GetInt("PlayerTowerEnter", -1);

        return (count >= 100);
    }
}
