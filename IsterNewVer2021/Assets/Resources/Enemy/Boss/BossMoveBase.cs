using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossMoveBase : BossPatternBase, IObjectCreator
{
    [SerializeField]
    protected BossMovable _movable;

    bool _patternStart;

    [SerializeField]
    protected GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    public override bool isPatternEnd { get { return _patternEnd; } }

    //첫 패턴 정보 초기화 및 선언
    public override void Start()
    {
        _patternStart = false;
        _patternEnd = false;
    }
    //패턴 업데이트 부분.
    public override void Update()
    {
        if (!_patternStart) return;

        if (_movable._moveStart) PatternOn();
        if (_movable._moveEnd) PatternEnd();
    }
    public override void PatternStart()
    {
        _patternStart = true;
        _patternEnd = false;
        _owner.movePatternStart = true;
    }
    public override void PatternEnd()
    {
        _patternStart = false;
        _owner.movePatternStart = false;
        _patternEnd = true;
    }
    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        return newBullet;
    }
}