using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerF1BossDoorCondition : DoorOpenConditionBase
    {
        private PlayerInventory _inventory;

    [SerializeField]
    private TowerF1Manager _manager;
    private int _targetID;

    void Start()
    {
        _inventory = FindObjectOfType<PlayerInventory>();

        switch (_manager.keyFlower.type)
        {
            case FlowerType.Slow:
                _targetID = 100;
            break;

            case FlowerType.AtkDecrease:
                _targetID = 101;
            break;

            case FlowerType.CoolTimeIncrease:
                _targetID = 102;
            break;

            case FlowerType.MoreDamage:
                _targetID = 103;
            break;
        }
    }

    public override bool IsCanOpenDoor()
    {
        bool isCanOpen = (_inventory.FindRuleItem(_targetID) != null);

        return isCanOpen;
    }

    public override void OpenTodo()
    {
        _inventory.RemoveRuleItem(_targetID);
    }
}
