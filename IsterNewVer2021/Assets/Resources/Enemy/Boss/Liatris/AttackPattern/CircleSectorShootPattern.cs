using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CircleSectorShootPattern : BossAttackBase
{
    //발사 위치
    [SerializeField]
    Transform _bulletFirePos;
    [SerializeField]
    Transform _dirCheckPos;

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
    float _currentCount;
    float _currentTime;

    float _checkTimer;

    [HideInInspector]
    public float _dotValue;

    Coroutine _corutine;

    Vector2 _dir;

    bool _isShoot;

    public override void SetPatternId()
    {
        _colliderObject.transform.position = _dirCheckPos.transform.position;
        _patternID = 100;
    }
    public override void Update()
    {
        if (!_owner.player) return;

        base.Update();

        Vector2 toPlayerDir = CommonFuncs.CalcDir(_dirCheckPos.position, _owner.player);
        float rotAngle = CommonFuncs.DirToDegree(toPlayerDir) - 270.0f;

        if (rotAngle >= 90.0f)
            rotAngle -= 360.0f;

        _colliderObject.transform.localRotation = Quaternion.identity;
        _colliderObject.transform.Rotate(new Vector3(0.0f, 0.0f, rotAngle));

        if (_isInside && _attacker._attackStart)
        {
            _checkTimer += IsterTimeManager.bossDeltaTime;
            if (_checkTimer > _insideTime) ShootStart();
        }

        if(_colliderObject.activeSelf) _dir = (_owner.player.center - _bulletFirePos.position).normalized;
    }

    public override void PatternStart()
    {
        base.PatternStart();
    }
    public override void PatternEnd()
    {
        base.PatternEnd();
        _colliderObject.SetActive(false);

        if (_corutine != null)
            StopCoroutine(_corutine);
    }
    public override void PatternOn()
    {
        _colliderObject.SetActive(true);
        _isShoot = false;
        _currentCount = 0;
        _currentTime = 0;
    }
    void ShootStart()
    {
        _isShoot = true;
        _corutine = StartCoroutine(ShootTheBullet());
        _checkTimer = 0;
    }
    public override void PatternOff()
    {
        _isShoot = false;
        _owner.attackPatternEnd = true;
    }

    IEnumerator ShootTheBullet()
    {
        _attacker._attackStart = false;
        _colliderObject.SetActive(false);

        while (_currentCount < _attackCount)
        {
            //if(_colliderObject.activeSelf) _colliderObject.SetActive(false);

            Shoot();

            while (_currentTime < _attackDelay)
            {
                _currentTime += IsterTimeManager.bossDeltaTime;
                yield return null;
            }
            _currentTime = 0;

            _currentCount++;

            yield return null;

        }

        PatternOff();

        yield return null;
    }
    void Shoot()
    {
        // Vector2 dir = CommonFuncs.CalcDir(transform.position, _player);
        float dot = Vector3.Dot(Vector2.right, _dir);
        float startAngle = Mathf.Acos(dot);

        if (_dir.y < 0.0f)
            startAngle = Mathf.PI * 2 - startAngle;

        float _angleCut = _angle / _fireCount;

        if (_sfx)
            _sfx.PlaySFX("shoot");

        for (int i = 0; i < _fireCount; ++i)
        {
            GameObject newBullet = CreateObject();
            newBullet.transform.position = _bulletFirePos.position;
            EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

            if (controller)
            {
                float angle = startAngle -  _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad;

                if (angle > Mathf.PI * 2)
                    angle -= Mathf.PI * 2;

                controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            }
            //count++;
        }
    }
  
}
