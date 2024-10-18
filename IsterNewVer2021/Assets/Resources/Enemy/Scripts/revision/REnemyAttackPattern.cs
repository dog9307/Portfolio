using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REnemyAttackPattern: REnemyAttackPatternBase
{
    public override void Update()
    {

    }
    public override void FireBullet()
    {
        throw new System.NotImplementedException();
    }
    public override void Reload()
    {
        _isShoot = false;
        _patternEnd = false;
    }
}
