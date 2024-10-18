using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DoorOpenNeedItem : DoorOpenConditionBase
{
    private PlayerInventory _inventory;

    [SerializeField]
    private int _targetKeyItem;

    public UnityEvent OnOpen;

    public override bool IsCanOpenDoor()
    {
        if (!_inventory)
            _inventory = FindObjectOfType<PlayerInventory>();

        bool isCanOpen = (_inventory.FindRelicItem(_targetKeyItem) != null);

        return isCanOpen;
    }

    public override void OpenTodo()
    {
        _inventory.RemoveRelicItem(_targetKeyItem);

        if (OnOpen != null)
            OnOpen.Invoke();
    }
}
