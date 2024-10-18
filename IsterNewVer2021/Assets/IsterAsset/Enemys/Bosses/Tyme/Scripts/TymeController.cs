using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TymeController : BossController
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

        if (currentSequence == null)
        {
            if (inMelee)
            {
                if (_movable.moveList.Count != 0)
                {
                    _patternSquence.Enqueue(_movable.moveList[0]);
                }
            }
            else
            {
                int patternNum = Random.Range(0, _attacker.attackList.Count);

                if (_attacker.attackList.Count != 0)
                {
                    _patternSquence.Enqueue(_attacker.attackList[patternNum]);
                }
            }

            _currentCoroutine = StartCoroutine(PatternStart());
        }
    }
}
