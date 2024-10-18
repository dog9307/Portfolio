using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangUlYeeMelee : TempBangUlYeePattern, IObjectCreator
{
    Coroutine _coroutine;

    [SerializeField]
    Transform _meleeAttackPos;

    public float _attackTimer;
    float _currentTimer;

    [SerializeField]
    protected GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    public bool _isMeleeOn;

    bool _patternStart;

    [SerializeField]
    ConditionalDoorTalkFrom _door;

    public float _bulletCreateTime;
    public float _floorScalemagnifi;
    public float _bulletSmallmagnifi;
    private void Start()
    {
        _patternStart = false;
    }
    private void Update()
    {
        if (_door)
        {
            if (!_door.GetComponent<Collider2D>().isActiveAndEnabled && !GetComponent<Damagable>().isDie)
            {
                if (_isMeleeOn && !_patternStart)
                {
                    _currentTimer += IsterTimeManager.deltaTime;
                    if (_currentTimer > _attackTimer)
                    {
                        PatternOn();
                    }
                }
                else if (!_isMeleeOn) _currentTimer = 0;
            }
        }
        else
        {
            if (!GetComponent<Damagable>().isDie)
            {
                if (_isMeleeOn && !_patternStart)
                {
                    _currentTimer += IsterTimeManager.deltaTime;
                    if (_currentTimer > _attackTimer)
                    {
                        PatternOn();
                    }
                }
                else if (!_isMeleeOn) _currentTimer = 0;
            }
        }
    }

    public  void PatternOn()
    {
        _patternStart = true;
        _currentTimer = 0;
        _coroutine = StartCoroutine(MeleeAttack());
    }
    public  void PatternOff()
    {
        _isMeleeOn = false;
        _patternStart = false;
        StopCoroutine(_coroutine);
    }
    IEnumerator MeleeAttack()
    {
        GameObject newBullet = CreateObject();
        newBullet.transform.position = _meleeAttackPos.position;
        newBullet.gameObject.GetComponent<ChargerGaugeWithTimer>()._maxTime = _bulletCreateTime;
        newBullet.transform.localScale = (newBullet.transform.localScale/_floorScalemagnifi);
        newBullet.gameObject.GetComponent<ChargerFloorBulletCreator>()._size = _bulletSmallmagnifi;

        yield return null;

        PatternOff();
    }
    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        return newBullet;
    }
}
