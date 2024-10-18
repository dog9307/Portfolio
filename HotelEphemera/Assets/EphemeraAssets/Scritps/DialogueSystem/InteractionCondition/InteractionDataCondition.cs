using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InteractionDataCondition : InteractionConditionBase
{
    [SerializeField]
    private string[] _relativeConditions;

    public override bool IsCanInteraction()
    {
        if (_relativeConditions == null)
            return false;

        bool isCorrectCondition = true;
        foreach (var s in _relativeConditions)
        {
            int count = SavableDataManager.instance.FindIntSavableData(s);
            if (count < 100)
            {
                isCorrectCondition = false;
                break;
            }
        }

        return isCorrectCondition;
    }
}
