using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverShootController : RemoverController
{
    public override void DestroyBullet()
    {
        Destroy(gameObject);
    }

    protected override void ComputeVelocity()
    {
        _targetVelocity = dir * speed;
    }
}
