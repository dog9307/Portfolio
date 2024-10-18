using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllimaAfterTowerFirst : DialogueNodeChangerCondition
{
    public override bool IsCanChange()
    {
        int count0 = PlayerPrefs.GetInt("PlayerTowerEnter", -1);
        int count1 = PlayerPrefs.GetInt("IllimaAfterTowerFirstDone", -1);

        return (count0 >= 100 && count1 < 100);
    }
}
