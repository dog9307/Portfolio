using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldGardenBossRoomDoorConditions : DoorOpenConditionBase
{
    public override bool IsCanOpenDoor()
    {
        return false;
    }

    public override void OpenTodo()
    {
    }
}
