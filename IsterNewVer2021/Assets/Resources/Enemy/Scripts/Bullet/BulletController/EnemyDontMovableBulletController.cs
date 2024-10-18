using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDontMovableBulletController : EnemyBulletController
{
    public override void Start()
    {
        _anim = GetComponent<Animator>();
        
    }
}
