using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSetCondition : InteractionConditionBase
{
    [SerializeField]
    private InteractionConditionBase[] _conditions;

    public override bool IsCanInteraction()
    {
        if (_conditions == null) return false;
        if (_conditions.Length <= 0) return false;

        bool isCanInteraction = true;
        foreach (var c in _conditions)
        {
            if (!c.IsCanInteraction())
            {
                isCanInteraction = false;
                break;
            }
        }

        return isCanInteraction;
    }
}
