using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamFloorPattern : BossAttackBase
{
    public float _attackCount;
    public float _attackDelay;

    public float _currentCount;
    public float _currentTime;

    Coroutine _corutine;

    public override void SetPatternId()
    {
        _patternID = 104;
    }
    public override void PatternOn()
    {
        _currentCount = 0;
        _currentTime = 0;
        _corutine = StartCoroutine(StreamFloor());
    }
    public override void PatternOff()
    {
        _owner.attackPatternEnd = true;
        StopCoroutine(_corutine);
    }
    IEnumerator StreamFloor()
    {
        _attacker._attackStart = false;

        //_bulletCreatePos = _owner.player.transform;
        Vector3 bulletPos = _owner.player.transform.position;

        while (_currentCount < _attackCount)
        {
            while (_currentTime < _attackDelay)
            {
                _currentTime += IsterTimeManager.bossDeltaTime;
                yield return null;
            }
            _currentTime = 0;
            _currentCount++;

            GameObject newBullet = CreateObject();
            newBullet.transform.position = bulletPos;

            yield return null;

        }

        PatternOff();
    }
}

