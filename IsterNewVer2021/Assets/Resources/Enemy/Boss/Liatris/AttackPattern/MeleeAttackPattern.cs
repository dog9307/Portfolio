using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackPattern : BossAttackBase
{
    Coroutine _coroutine;

    [SerializeField]
    Transform _meleeAttackPos;

    [SerializeField]
    private EnemyAttackEffector _effector;

    public override void SetPatternId()
    {
        _patternID = 101;
    }
    public override void PatternOn()
    {
        if (!_effector)
            _effector = GetComponentInChildren<EnemyAttackEffector>();

        _coroutine = StartCoroutine(MeleeAttack());
    }
    public override void PatternOff()
    {
        _owner.attackPatternEnd = true;
        StopCoroutine(_coroutine);
    }
    IEnumerator MeleeAttack()
    {
        if (_effector)
            _effector.StartAttackEffect();

        _attacker._attackStart = false;

        GameObject newBullet = CreateObject();
        newBullet.transform.position = _meleeAttackPos.position;

        _sfx.PlaySFX("melee");

        yield return null;

        PatternOff();
    }
}
