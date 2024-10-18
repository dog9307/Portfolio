using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionJustFalseCondition : InteractionConditionBase
{
    public override bool IsCanInteraction()
    {
        return false;
    }
}
