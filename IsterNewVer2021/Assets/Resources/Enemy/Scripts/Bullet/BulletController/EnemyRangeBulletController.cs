using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeBulletController : EnemyBulletController
{ 
    public override void Start()
    {
        base.Start();
    }
    public override void DestroyBullet()
    {
       _anim.SetTrigger("effectStart");
    }
}
