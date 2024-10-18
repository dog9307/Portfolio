using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Area1FieldGardenDoorCondition : DoorOpenConditionBase
{
    public bool isCanOpen { get; set; } = false;

    public UnityEvent OnOpenDoor;

    public override bool IsCanOpenDoor()
    {
        return isCanOpen;
    }

    public override void OpenTodo()
    {
        if (OnOpenDoor != null)
            OnOpenDoor.Invoke();
    }
}
