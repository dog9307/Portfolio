using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneNPCAfterTower : DialogueNodeChangerCondition
{
    public override bool IsCanChange()
    {
        int count = PlayerPrefs.GetInt("PlayerTowerEnter", -1);

        return (count >= 100 && (PlayerPrefs.GetInt("StoneAfterTowerFirstMeetDone", -1) >= 100));
    }
}
