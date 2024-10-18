using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerSectorCircle : MonoBehaviour, IObjectCreator
{
    public PlayerMoveController _player;

    public LiatrisFlowers _liatrisDamagable;

    [SerializeField]
    protected GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    [SerializeField]
    Transform _bulletPos;

    //체크 콜라이더.
    public GameObject _colliderObject;

    //발사 각
    [HideInInspector]
    public float _angle;

    //발사수 
    public float _fireCount;

    //공격횟수, 딜레이
    public float _attackCount;
    public float _attackDelay;
    //발사까지 걸리는 시간
    public float _fireTime;

    //현재 공격횟수 및 시간
    public float _attackTimer;
    [SerializeField]
    float _currentCount;
    [SerializeField]
    float _currentTime;

    [HideInInspector]
    public Coroutine _corutine;

    //장판 방향
    [HideInInspector]
    public Vector2 _dir;
    //발사 방향
    [HideInInspector]
    public Vector2 _fireDir;
    //발사했나?
    [HideInInspector]
    public bool _isShoot;

    public float _coolTime;
    float _currentCoolTime;

    bool _patternStart;
    float _firstPatternTime;
    float _currentPatternTime;

    // Start is called before the first frame update
    void Start()
    {
        _patternStart = false;
        _firstPatternTime = 2;
        _currentPatternTime = 0;
        _angle = 270.0f;
        _player = FindObjectOfType<PlayerMoveController>();
        //_liatrisDamagable = GetComponent<LiatrisFlowers>();
        //_corutine = StartCoroutine(CoolTimeSectorCircle());
    }

    // Update is called once per frame
    void Update()
    {
        if (!_liatrisDamagable._damagable.isDie)
        {
            if (!_colliderObject.activeSelf && _liatrisDamagable._isSpawned)
            {
                if (!_patternStart)
                {
                    _currentPatternTime += IsterTimeManager.enemyDeltaTime;
                    if (_currentPatternTime > _firstPatternTime)
                    {
                        _patternStart = true;
                        _corutine = StartCoroutine(CoolTimeSectorCircle());
                    }
                }

                if (!_isShoot) _fireDir = _dir;
            }
            else return;
        }
        else
        {
            if (_corutine != null)
            {
                StopCoroutine(_corutine);
            }

            if (_colliderObject.activeSelf)
            {
                if (_colliderObject.GetComponent<FlowerSectorCircleWarning>()._coroutine != null)
                {
                    _colliderObject.GetComponent<FlowerSectorCircleWarning>().StopCoroutine(_corutine);
                    _colliderObject.SetActive(false);
                }
            }
        }
    }

    //protected override void Update()
    //{
    //    base.Update();
    //}
    //
    //protected override void FlowerUpdate()
    //{
    //    if (_isSpawned)
    //    {
    //        _damagable.isCanHurt = true;
    //
    //        if (!_damagable.isDie)
    //        {
    //            if (!_colliderObject.activeSelf && _isSpawned)
    //            {
    //                if (!_patternStart)
    //                {
    //                    _currentPatternTime += IsterTimeManager.enemyDeltaTime;
    //                    if (_currentPatternTime > _firstPatternTime)
    //                    {
    //                        _patternStart = true;
    //                        _corutine = StartCoroutine(CoolTimeSectorCircle());
    //                    }
    //                }
    //
    //                if (!_isShoot) _fireDir = _dir;
    //            }
    //            else return;
    //        }
    //    }
    //    else
    //        _damagable.isCanHurt = false;
    //}
    //protected override void FlowerDie()
    //{
    //    if (_corutine != null)
    //    {
    //        StopCoroutine(_corutine);
    //    }
    //
    //    if (_colliderObject.activeSelf)
    //    {
    //        if (_colliderObject.GetComponent<FlowerSectorCircleWarning>()._coroutine != null)
    //        {
    //            _colliderObject.GetComponent<FlowerSectorCircleWarning>().StopCoroutine(_corutine);
    //            _colliderObject.SetActive(false);
    //        }
    //    }
    //}
    //
    public IEnumerator ShootTheBullet()
    {
        _colliderObject.SetActive(false);

        while (_currentCount < _attackCount)
        {
            while (_currentTime < _attackDelay)
            {
                _currentTime += IsterTimeManager.enemyDeltaTime;

                yield return null;
            }
            _currentTime = 0;

            _currentCount++;

            Shoot();

            yield return null;
           
        }

        //StopCoroutine(_corutine);

        _corutine = StartCoroutine(CoolTimeSectorCircle());
        
        yield return null;
    }
    void Shoot()
    {
        float _angleCut = _angle / _fireCount;
        float startAngle = CommonFuncs.DirToDegree(_dir);

        for (int i = 0; i < _fireCount; ++i)
        {
            GameObject newBullet = CreateObject();
            newBullet.transform.position = _bulletPos.position;
            EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

            if (controller)
            {
                float angle = startAngle - _angleCut * (_fireCount / 2) + _angleCut * i;

                if (angle > 360.0f)
                    angle -= 360.0f;

                if (angle < 0.0f)
                    angle += 360.0f;

                controller.dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
            }
        }
    }
    IEnumerator CoolTimeSectorCircle()
    {
        _currentCount = 0;
        _currentTime = 0;
            
        while (_currentCoolTime < _coolTime)
        {
            _currentCoolTime += IsterTimeManager.enemyDeltaTime;
            yield return null;
        }
        _currentCoolTime = 0;

        _colliderObject.SetActive(true);

        yield return null;

    }
    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        return newBullet;
    }
}
