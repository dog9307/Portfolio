using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicDisposableObject : DisposableObject
{
    public override void AlreadyUsed()
    {
        base.AlreadyUsed();

        Destroy(gameObject);
    }
}
