using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnOff : MonoBehaviour
{
    public void LockOffEnd()
    {
        transform.parent.GetComponentInParent<RoomInfo>()._isCanSelect = true;
        RoomManager.instance.ButtonsUnfreeze();

        DestroyObject(this);
    }
}
