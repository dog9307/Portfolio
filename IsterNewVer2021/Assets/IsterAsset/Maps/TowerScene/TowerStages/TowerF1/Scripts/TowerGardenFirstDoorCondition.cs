using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGardenFirstDoorCondition : DoorOpenConditionBase
{
    private PlayerPassiveSkillStorage _storage;

    public override bool IsCanOpenDoor()
    {
        if (!_storage)
            _storage = FindObjectOfType<PlayerPassiveSkillStorage>();

        if (!_storage)
            return false;

        return _storage.IsExist(210);
    }

    public override void OpenTodo() { }
}
