using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NellBlockOpusCondition : ColliderTriggerCondition
{
    public override bool IsCanTrigger()
    {
        bool isCutSceneCanStart = false;

        isCutSceneCanStart =
            (SavableDataManager.instance.FindIntSavableData("HelpTheNell") >= 100) &&
            ((SavableDataManager.instance.FindIntSavableData("Area_1_Field_2_Stair_Up") < 100) ||
            (SavableDataManager.instance.FindIntSavableData("FieldLiatrisStoneAllGain") < 100));

        return isCutSceneCanStart;
    }
}
