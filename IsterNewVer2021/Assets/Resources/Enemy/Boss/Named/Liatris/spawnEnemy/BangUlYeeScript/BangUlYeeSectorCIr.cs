using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangUlYeeSectorCIr : TempBangUlYeePattern, IObjectCreator
{
    public PlayerMoveController _player;

    [SerializeField]
    protected GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    [SerializeField]
    ConditionalDoorTalkFrom _door;
    //발사 위치
    [SerializeField]
    Transform _bulletPos;

    //발사 수
    public float _fireCount;

    //공격횟수, 딜레이
    public float _attackCount;
    public float _attackDelay;

    //체크 콜라이더.
    public GameObject _colliderObject;
    //각도 마스크
    public Transform _right;
    public Transform _left;

    //시야각 범위 내부에 있는지 판별   
    public float _angle;
    public bool _isInside;
    public float _insideTime;

    //현재 공격횟수 및 시간
    public float _attackTimer;
    float _currentCount;
    float _currentTime;

    float _checkTimer;

    bool _patternStart;

    [HideInInspector]
    public float _dotValue;

    Coroutine _corutine;

    [HideInInspector]
    public Vector2 _dir;

    [SerializeField]
    private Damagable _damagable;

    bool _isShoot;
    private void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();
        _patternStart = false;
    }
    public void Update()
    {
        if (!_player) return;

        if (!_door.GetComponent<Collider2D>().isActiveAndEnabled && GetComponent<BangUlYeeAttacker>().IsInRange())
        {
            Vector2 toPlayerDir = CommonFuncs.CalcDir(transform, _player.transform);
            float rotAngle = CommonFuncs.DirToDegree(toPlayerDir) - 270.0f;

            if (rotAngle >= 90.0f)
                rotAngle -= 360.0f;

            _colliderObject.transform.localRotation = Quaternion.identity;
            _colliderObject.transform.Rotate(new Vector3(0.0f, 0.0f, rotAngle));

            if (_isInside && _patternStart)
            {
                _checkTimer += IsterTimeManager.enemyDeltaTime;
                if (_checkTimer > _insideTime) ShootStart();
            }


            if (!_isShoot && !_patternStart)
            {
                _currentTime += IsterTimeManager.enemyDeltaTime;
                if (_currentTime > _attackTimer)
                {
                    PatternOn();
                }
            }

            if (!_isShoot) _dir = toPlayerDir;
        }
        else return;
    }
    
    public void PatternOn()
    {
        _patternStart = true;
        _colliderObject.SetActive(true);
        _currentCount = 0;
        _currentTime = 0;
    }
    void ShootStart()
    {
        _isShoot = true;
        _corutine = StartCoroutine(ShootTheBullet());
        _currentCount = 0;
        _checkTimer = 0;
    }
    public void PatternOff()
    {
        _isShoot = false;
        _patternStart = false;
        _currentTime = 0;
    }
    public override void PatternEnd()
    {
        PatternOff();

        _colliderObject.SetActive(false);

        if (_corutine != null)
            StopCoroutine(_corutine);
    }

    IEnumerator ShootTheBullet()
    {
        _patternStart = false;
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

        PatternOff();

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
    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        return newBullet;
    }
}