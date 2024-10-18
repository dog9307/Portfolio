using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryedBulletMovable : EnemyBulletController
{
    public delegate void DestroyEvent();
    
    public DestroyEvent destroy { get; set; }

    public override void DestroyBullet()
    {
        if (destroy != null)
            destroy();
    }

    protected override void ComputeVelocity()
    {
        _targetVelocity = dir * speed;
    }
}
