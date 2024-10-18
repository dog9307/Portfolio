using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RemoverController : BulletMovable
{
    public PassiveRemoverUser user { get; set; }
    public Vector2 dir { get; set; }

    void Start()
    {
        isHide = false;

        EventTimer timer = GetComponent<EventTimer>();
        if (timer)
        {
            timer.totalTime *= user.timeMultiplier;
            timer.AddEvent(DestroyBullet);
        }
    }
}
