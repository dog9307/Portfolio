using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DoorOpenConditionBase : MonoBehaviour
{
    public abstract bool IsCanOpenDoor();
    public abstract void OpenTodo();
}
