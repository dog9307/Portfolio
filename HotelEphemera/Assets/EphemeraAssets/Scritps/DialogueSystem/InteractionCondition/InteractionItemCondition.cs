using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionItemCondition : InteractionConditionBase
{
    [SerializeField]
    private int _itemID = 0;

    public override bool IsCanInteraction()
    {
        // 아이템 시스템 생성 후 작업
        return ItemGainPopup.instance.IsItemInInventory(_itemID);
    }
}
