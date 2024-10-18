using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherShootController : BulletMovable
{
    public Vector2 dir { get; set; }

    void Start()
    {
        isHide = false;

        EventTimer timer = GetComponent<EventTimer>();
        if (timer)
            timer.AddEvent(DestroyBullet);
    }

    public override void DestroyBullet()
    {
        Destroy(gameObject);
    }

    protected override void ComputeVelocity()
    {
        _targetVelocity = dir * speed;
    }
}
