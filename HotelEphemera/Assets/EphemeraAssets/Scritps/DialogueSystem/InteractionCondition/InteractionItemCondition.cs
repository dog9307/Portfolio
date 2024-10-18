using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionItemCondition : InteractionConditionBase
{
    [SerializeField]
    private int _itemID = 0;

    public override bool IsCanInteraction()
    {
        // ������ �ý��� ���� �� �۾�
        return ItemGainPopup.instance.IsItemInInventory(_itemID);
    }
}
