using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGardenDoorCondition : DoorOpenConditionBase
{
    [SerializeField]
    private bool _isAlwaysFalse = false;

    private PlayerInventory _inventory;

    public override bool IsCanOpenDoor()
    {
        if (_isAlwaysFalse)
            return false;

        if (!_inventory)
            _inventory = FindObjectOfType<PlayerInventory>();

        if (!_inventory)
            return false;

        TowerGardenFinalKey key = _inventory.FindRuleItem(TowerGardenFinalKey.TowerGardenKeyId) as TowerGardenFinalKey;

        return (key != null);
    }

    public override void OpenTodo()
    {
        if (!_inventory)
            _inventory = FindObjectOfType<PlayerInventory>();

        _inventory.RemoveRuleItem(TowerGardenFinalKey.TowerGardenKeyId);
    }
}
