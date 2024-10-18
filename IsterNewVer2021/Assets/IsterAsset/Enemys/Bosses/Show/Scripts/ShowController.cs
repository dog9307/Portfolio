using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowController : BossController
{
    [SerializeField]
    protected SFXPlayer _sfx;
    protected override void Update()
    {
        base.Update();

        if (_bossMain.damagable.isHurt)
        {
            if (_sfx) _sfx.PlaySFX("hitted");
        }
    }
    protected override void PatternSetter()
    {
        base.PatternSetter();

        
    }
}
