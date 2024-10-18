using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherSpiritController : BossControllerBase
{
    //웨이브나 페이즈 관련 된 스크립트
    MotherSpirit _mother;

    MotherSpiritSpawnController _spawnContoller;

    Damagable _damagable;

    [SerializeField]
   // BulletCreator _creator;

    public bool _grogi;
    [HideInInspector]
    public bool _grogiTimeOver;
    public bool _spawnStart { get; set; }
    public bool _isSleep;
    [HideInInspector]
    public bool _attackable;
    [HideInInspector]
    public bool _patternStart;
    [HideInInspector]
    public bool _patternStartOnce;

    [SerializeField]
    public List<GameObject> _sauronRight = new List<GameObject>();
    [SerializeField]
    public List<GameObject> _sauronLeft = new List<GameObject>();
    [SerializeField]
    public List<GameObject> _sauronUp = new List<GameObject>();
    [SerializeField]
    public List<GameObject> _sauronDown = new List<GameObject>();

    public float _attackTimer;
    public float _currentTimer;

    public float _grogiTimer;
    public float _currentGrogiTimer;

    public int _HitStack;

    public float hpLimit { get { return _damagable.totalHP * (3 - _mother._phaseCount) * (1.0f / 3.0f); } }

    [SerializeField]
    private GameObject _meleeZone;

    [SerializeField]
    private ParticleSystem _bossRoomAmbient;

    [SerializeField]
    private MotherSpiritAniController _anim;

    // Start is called before the first frame update
    void Start()
    {
        //SetBossController(_mother);

        _mother = GetComponent<MotherSpirit>();
        _damagable = GetComponent<Damagable>();
        _spawnContoller = GetComponent<MotherSpiritSpawnController>();

        _HitStack = 0;
        _isSleep = true;
        _grogi = false;
        _grogiTimeOver = false;
        _patternStartOnce = false;
        _damagable.isCanHurt = false;

        _meleeZone.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (!_damagable.isDie)
        {
            _mother._phaseCount = _HitStack;
            if (_isSleep)
            {
                if (Input.GetKeyDown(KeyCode.F9))
                    BossWakeUp();
            }

            if (_patternStart && _spawnContoller._isSpawnEnd)
            {
                if (!_attackable && !_grogi && !_patternStartOnce)
                {
                    _currentTimer += IsterTimeManager.bossDeltaTime;
                    if (_currentTimer > _attackTimer)
                    {
                        _patternStartOnce = true;
                        _attackable = true;
                        _currentTimer = 0;
                    }
                }

                if (_grogi)
                {
                    _currentTimer = 0;
                    _damagable.isCanHurt = true;

                    _currentGrogiTimer += IsterTimeManager.bossDeltaTime;

                    if (_currentGrogiTimer > _grogiTimer)
                    {
                        _grogiTimeOver = true;
                        _grogi = false;
                        _currentGrogiTimer = 0;
                        _damagable.currentHP += _damagable.totalHP / 3.0f;
                    }
                }
                else
                {
                    _grogi = _spawnContoller.AllEnemyDie();
                    if (_grogi)
                    {
                        _anim.Grogi();
                    }
                }
            }

            if (_HitStack > 2.1)
            {
                _damagable.currentHP = 0;
            }
        }
    }
    public void StateResetWithHitted()
    {
        _currentTimer = 0;
        _currentGrogiTimer = 0;
        _mother._phaseCount += 1;
        _grogi = false;
        _damagable.isCanHurt = false;
        _spawnContoller._isSpawnEnd = false;
        _spawnStart = true;
    }
    public void StateResetWithTime()
    {
        _currentGrogiTimer = 0;
        _grogiTimeOver = false;
        _currentTimer = 0;
        _grogi = false;
        _spawnContoller._isSpawnEnd = false;
        _damagable.isCanHurt = false;
        _damagable.currentHP = hpLimit;
        _spawnStart = true;
    }
    public void HealAllChildren()
    {
        for (int i = 0; i < _spawnContoller._spawnEnemys.Count; i++)
        {
            if (!_spawnContoller._spawnEnemys[i].gameObject) continue;

            Damagable hp = _spawnContoller._spawnEnemys[i].GetComponent<Damagable>();
            if (_mother._phaseCount == 0)
            {
                Damage damage = DamageCreator.Create(_mother.gameObject, -(hp.totalHP / 10), 0, 1, 0);
                hp.HitDamager(damage, Vector2.zero);
            }
            else if (_mother._phaseCount == 1)
            {
                Damage damage = DamageCreator.Create(_mother.gameObject, -(hp.totalHP / 7), 0, 1, 0);
                hp.HitDamager(damage, Vector2.zero);
            }
            else if (_mother._phaseCount == 2)
            {
                Damage damage = DamageCreator.Create(_mother.gameObject, -(hp.totalHP / 4), 0, 1, 0);
                hp.HitDamager(damage, Vector2.zero);
            }
        }
    }
    public override void BossWakeUp()
    {
        _isSleep = false;
        _spawnStart = true;

        _meleeZone.SetActive(true);
    }

    public override void BossDie()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer)
        {
            renderer.sortingLayerName = "Foreground";
            renderer.sortingOrder = BlackMaskController.BLACKMASK_ORDER_IN_LAYER + 1;
        }

        SauronAllStop();
    }

    public override void BossDisappear()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer)
            renderer.sortingLayerName = "Dynamic";
    }

    public void SauronAllStop()
    {
        foreach (GameObject s in _sauronRight)
        {
            SauronLaserController laser = s.GetComponent<SauronLaserController>();
            if (laser)
                laser.LaserOff();
        }
        foreach (GameObject s in _sauronLeft)
        {
            SauronLaserController laser = s.GetComponent<SauronLaserController>();
            if (laser)
                laser.LaserOff();
        }
        foreach (GameObject s in _sauronUp)
        {
            SauronLaserController laser = s.GetComponent<SauronLaserController>();
            if (laser)
                laser.LaserOff();
        }
        foreach (GameObject s in _sauronDown)
        {
            SauronLaserController laser = s.GetComponent<SauronLaserController>();
            if (laser)
                laser.LaserOff();
        }
    }

    public void LiatrisSubAllCut()
    {
        foreach(GameObject _object in _sauronRight)
        {
            _object.gameObject.SetActive(false);
        }
        foreach (GameObject _object in _sauronLeft)
        {
            _object.gameObject.SetActive(false);
        }
        foreach (GameObject _object in _sauronUp)
        {
            _object.gameObject.SetActive(false);
        }
        foreach (GameObject _object in _sauronDown)
        {
            _object.gameObject.SetActive(false);
        }

        _meleeZone.gameObject.SetActive(false);
        if (_bossRoomAmbient)
        {
            var ps = _bossRoomAmbient;
            ps.loop = false;
        }
    }
}
