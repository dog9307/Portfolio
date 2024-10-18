using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossAttackBase : BossPatternBase , IObjectCreator
{
    [SerializeField]
    protected BossAttacker _attacker;

    bool _patternStart;
    //[SerializeField]
    //protected Transform _bulletCreatePos;

    [SerializeField]
    protected GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    public override bool isPatternEnd { get { return _patternEnd; } }


    //첫 패턴 정보 초기화 및 선언
    public override void Start()
    {
        _patternStart = false;
        _patternEnd = false;
        SetPatternId();
    }
    //패턴 업데이트 부분.
    public override void Update()
    {
        if (!_patternStart) return;

        if (_attacker._attackStart) PatternOn();
        if (_attacker._attackEnd) PatternEnd();
    }
    public override void PatternStart()
    {
        _patternStart = true;
        _patternEnd = false;
        _owner.attackPatternStart = true;

        if (_sfx)
            _sfx.PlaySFX(_patternStartSfx);
    }
    public override void PatternEnd()
    {
        _patternStart = false;
        _patternEnd = true;
        _owner.attackPatternStart = false;

        if (_sfx)
            _sfx.PlaySFX(_patternEndSfx);
    }
    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        //newBullet.transform.position = _bulletCreatePos.position;
        return newBullet;
    }
    public abstract void SetPatternId();
}
