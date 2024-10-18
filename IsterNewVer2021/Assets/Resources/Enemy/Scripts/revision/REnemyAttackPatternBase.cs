using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RangeAttackSetter
{
    [Header("한번에 발사되는 탄막 수")]
    public int _fireCount;
    [Header("연발일 경우 입력 아닐시 0")]
    public int _repeatCount;
    [Header("bullet 발사각 변경")]
    public float _turnAngle;
    [Header("발사 주기")]
    public float _intervalTimer;
    [Header("최대 발사각 0 ~ 180")]
    [Range(0, 180)]
    public float _angleMax;
    [Header("wave 반복 수 (기본 0)")]
    public int _waveCount;
}

[System.Serializable]
public struct MeleeAttackSetter
{
    [Header("연발일 경우 입력 아닐시 0")]
    public int _repeatCount;
    [Header("발사 주기")]
    public float _intervalTimer;
}

[System.Serializable]
public abstract class REnemyAttackPatternBase : RPatternBase
{
    [HideInInspector]
    public AP attackType;
    
    [HideInInspector]
    public REnemyAttacker _attacker;
    
    //paternEnd 판별용 (불릿 생성 됐는가)
    public REnemyBulletCreator _creator;
    public float _fireCount;
    public bool _isShoot;

    public override void Init()
    {
        _isShoot = false;
        _patternEnd = false;
       // _fireCount = _owner.attacker._rangeAttackSetter._fireCount;
    }
    public override void Update()
    {
        if (!_attacker && _owner)
            _attacker = _owner.attacker;
        else
            return;
    }

    public override void PatternStart()
    {
        Reload();
    }
    public override void PatternEnd()
    {
        _patternEnd = true;
       _owner.AttackerOff();
       //_isShoot = false;
    }
    public abstract void Reload();
    public abstract void FireBullet();
}
