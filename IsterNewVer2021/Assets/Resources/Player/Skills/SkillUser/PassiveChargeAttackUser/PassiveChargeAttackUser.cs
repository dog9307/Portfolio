using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveChargeAttackUser : SwordChangingUser, IObjectCreator, ICoolTime
{
    [SerializeField]
    private AttackChargeEffectController _effect;

    [SerializeField]
    private float _totalTime = 5.0f;
    [SerializeField]
    private float _minTime = 0.3f;
    private float _currentTime;
    public float totalTime { get { return _totalTime; } }

    [SerializeField]
    private float _minDistance = 2.0f;
    [SerializeField]
    private float _maxDistance = 6.0f;
    private float _currentDistance;

    [SerializeField]
    private float _minDamage = 20.0f;
    [SerializeField]
    private float _maxDamage = 30.0f;
    private float _currentDamage;

    private bool _isChargingStart;
    public bool isChargingStart { get { return _isChargingStart; } }
    private AudioSource _chargeAudio;

    private PlayerMoveController _move;
    private PlayerDashDelay _delay;
    private PlayerSkillUsage _skill;
    private PlayerAnimController _anim;
    private LookAtMouse _look;
    private PlayerInfo _playerInfo;
    private DebuffInfo _debuff;
    private float _prevSpeed;

    public HAND relativeHand { get; set; }
    public string attackKey { get; set; }

    [SerializeField]
    private GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    [SerializeField]
    protected float _totalCoolTime = 0.3f;
    protected float _currentCoolTime;

    public virtual float currentCoolTime { get { return _currentCoolTime; } set { _currentCoolTime = value; } }
    public virtual float totalCoolTime
    {
        get
        {
            if (_debuff)
                return _totalCoolTime * _debuff.coolTimeMultiplier;
            return _totalCoolTime;
        }
        set { _totalCoolTime = value; }
    }

    [SerializeField]
    private LayerMask _obstacles;

    [HideInInspector]
    [SerializeField]
    private PassiveChargeRangeHelper _helper;

    private PlayerFlyingChecker _flyingChecker;

    protected override void Start()
    {
        base.Start();

        SwordChange();
        _attack.ChangeHelper(new PlayerChargeAttackHelper());

        Init();

        _skill.chargeUser = this;
        _playerInfo = _attack.GetComponent<PlayerInfo>();
        _debuff = _playerInfo.GetComponent<DebuffInfo>();

        _currentCoolTime = totalCoolTime;
    }

    void OnDestroy()
    {
        _skill.chargeUser = null;
    }

    public override void Init()
    {
        _move = _attack.GetComponent<PlayerMoveController>();
        _delay = _attack.GetComponent<PlayerDashDelay>();
        _skill = _attack.GetComponent<PlayerSkillUsage>();
        _anim = _attack.GetComponent<PlayerAnimController>();
        _look = _attack.GetComponent<LookAtMouse>();

        _prevSpeed = _move.GetSpeed();
    }

    void Update()
    {
        float speed = _prevSpeed;
        if (!_isChargingStart)
        {
            if (_attack.IsCanAttack() && !IsCoolTime())
            {
                if (KeyManager.instance.IsStayKeyDown("attack_left"))
                {
                    bool isDelay = false;
                    relativeHand = HAND.LEFT;
                    isDelay = _delay.isLeftDelay;

                    ChargingTriggered(isDelay);
                }
                //else if (KeyManager.instance.IsStayKeyDown("attack_right"))
                //{
                //    bool isDelay = false;
                //    relativeHand = HAND.RIGHT;
                //    isDelay = _delay.isRightDelay;

                //    ChargingTriggered(isDelay);
                //}

                if (_isChargingStart)
                {
                    if (relativeHand == HAND.LEFT)
                        attackKey = "attack_left";
                    else if (relativeHand == HAND.RIGHT)
                        attackKey = "attack_right";

                    if (_helper)
                        _helper.StartFading(0.7f, _minTime * 3.0f);
                }
            }
        }
        else
        {
            if (_currentTime < _totalTime)
            {
                _currentTime += IsterTimeManager.deltaTime;
                if (_currentTime < _totalTime)
                    _effect.EffectOn(0);
                else
                    _effect.EffectOn(1);
            }

            float ratio = _currentTime / _totalTime;
            if (ratio > 1.0f)
                ratio = 1.0f;
            _currentDistance = Mathf.Lerp(_minDistance, _maxDistance, ratio);
            _currentDamage = Mathf.Lerp(_minDamage, _maxDamage, ratio);

            if (_helper)
                _helper.ApplyScale(_currentDistance);

            speed = 0.0f;

            if (!KeyManager.instance.IsStayKeyDown(attackKey))
            {
                if (_currentTime < _minTime)
                    Attack();
                else
                    ChargeAttack();

                CoolTimeStart();
            }
        }

        _anim.AttackCharging(_isChargingStart);
        _move.speed = speed;
    }

    public void EffectOn(bool isOn)
    {
        if (_effect)
        {
            if (!isOn)
                _effect.EffectOff();
        }
    }


    public void Attack()
    {
        ChargeCancle(true);

        //_attack.AttackTriggered(ATTACK_TYPE.NORMAL, relativeHand);
        if (!_attack.equipment.isAttacking)
            _attack.AttackStart();
    }

    public void ChargeAttack()
    {
        ChargeCancle(true);

        _attack.SwordAppear();

        _anim.ChargeAttack();

        GameObject newBullet = CreateObject();

        Vector3 playerPos = _move.center;
        Vector3 dir = _look.dir;
        dir.z = 0.0f;

        float scaleFactor = PlayerReposition(playerPos, dir, _currentDistance);

        Vector3 scale = newBullet.transform.localScale;
        scale.x = _currentDistance * scaleFactor;
        newBullet.transform.localScale = scale;

        float angle = CommonFuncs.DirToDegree(dir);
        newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        newBullet.transform.position = playerPos + (dir * (_currentDistance * scaleFactor) / 2.0f);

        Damager damager = newBullet.GetComponentInChildren<Damager>();

        Damage realDamage = damager.damage;
        realDamage.owner = gameObject;
        realDamage.damage = _playerInfo.attackFigure;
        realDamage.additionalDamage = 0.0f;
        realDamage.damageMultiplier = _currentDamage * buff.damageMultiplier * _debuff.damageDecreaseMultiplier;
        damager.damage = realDamage;
    }

    float PlayerReposition(Vector3 playerPos, Vector3 dir, float distance)
    {
        float realDistance = distance;
        RaycastHit2D[] hits = Physics2D.RaycastAll(playerPos, dir, distance, _obstacles);
        foreach (var hit in hits)
        {
            if (hit.collider.isTrigger) continue;

            if (hit.distance < realDistance)
                realDistance = hit.distance;
        }

        Vector3 playerTargetPos = playerPos + dir * (realDistance - 0.5f);

        _move.dashStartPos = _move.transform.position;
        _move.Move(playerTargetPos);

        if (!_flyingChecker)
            _flyingChecker = FindObjectOfType<PlayerFlyingChecker>();

        StartCoroutine(FlyingCheck());

        float scaleFactor = realDistance / distance;
        return scaleFactor;
    }

    IEnumerator FlyingCheck()
    {
        yield return new WaitForSeconds(0.5f);

        _flyingChecker.Check();
    }

    public void ChargeCancle(bool isEffectOff = false)
    {
        if (_chargeAudio)
        {
            SoundSystem.instance.StopLoopSFX(_chargeAudio);
            _chargeAudio = null;
        }

        _isChargingStart = false;
        _move.speed = _prevSpeed;

        if (isEffectOff)
            EffectOn(false);

        if (_helper)
            _helper.StartFading(0.0f, 0.1f);
    }

    void ChargingTriggered(bool isDelay)
    {
        if (!_skill.isSkillUsing)
        {
            if (!isDelay)
                UseSkill();
        }
    }

    public override void UseSkill()
    {
        EffectOn(true);

        StartCoroutine(SoundDelay());

        _isChargingStart = true;
        _currentTime = 0.0f;

        _currentDistance = _minDistance;
        _currentDamage = _minDamage;
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(0.3f);

        if (_chargeAudio)
        {
            SoundSystem.instance.StopLoopSFX(_chargeAudio);
            _chargeAudio = null;
        }

        if (_isChargingStart)
            _chargeAudio = _sfx.PlayLoopSFX("charging");
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate(effectPrefab);

        return newBullet;
    }

    public virtual void CoolTimeStart()
    {
        currentCoolTime = 0.0f;
        StartCoroutine(CoolTimeUpdate());
    }

    public virtual IEnumerator CoolTimeUpdate()
    {
        while (currentCoolTime < totalCoolTime)
        {
            currentCoolTime += IsterTimeManager.deltaTime;
            yield return null;
        }
        currentCoolTime = totalCoolTime;
    }

    public virtual void CoolTimeDown(float time)
    {
        currentCoolTime += time;
    }

    public virtual bool IsCoolTime()
    {
        return (currentCoolTime < totalCoolTime);
    }
}

// stacking charge
//public class PassiveChargeAttackUser : SkillUser, IObjectCreator
//{
//    [SerializeField]
//    private AttackChargeEffectController _effect;

//    [SerializeField]
//    private float _stackTime;
//    public float stackTime { get { return _stackTime; } set { _stackTime = value; } }

//    private float _currentTime;

//    [SerializeField]
//    private int _maxStack = 2;
//    public int maxStack { get { return _maxStack; } set { _maxStack = value; } }

//    public int currentStack { get; set; }

//    public float totalTime { get { return _stackTime * maxStack; } }

//    private bool _isChargingStart;
//    public bool isChargingStart { get { return _isChargingStart; } }

//    private PlayerAttacker _attack;
//    private PlayerMoveController _move;
//    private PlayerDashDelay _delay;
//    private PlayerSkillUsage _skill;
//    private PlayerAnimController _anim;
//    private LookAtMouse _look;
//    private float _prevSpeed;

//    public HAND relativeHand { get; set; }
//    public string attackKey { get; set; }

//    [SerializeField]
//    private GameObject _prefab;
//    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

//    [SerializeField]
//    private LayerMask _obstacles;

//    [HideInInspector]
//    [SerializeField]
//    private PassiveChargeRangeHelper _helper;

//    protected override void Start()
//    {
//        base.Start();

//        Init();
//    }

//    public override void Init()
//    {
//        _attack = FindObjectOfType<PlayerAttacker>();
//        _move = _attack.GetComponent<PlayerMoveController>();
//        _delay = _attack.GetComponent<PlayerDashDelay>();
//        _skill = _attack.GetComponent<PlayerSkillUsage>();
//        _anim = _attack.GetComponent<PlayerAnimController>();
//        _look = _attack.GetComponent<LookAtMouse>();

//        _prevSpeed = _move.GetSpeed();
//    }

//    void Update()
//    {
//        float speed = _prevSpeed;
//        if (!_isChargingStart)
//        {
//            if (_attack.IsCanAttack())
//            {
//                if (KeyManager.instance.IsStayKeyDown("attack_left"))
//                {
//                    bool isDelay = false;
//                    relativeHand = HAND.LEFT;
//                    isDelay = _delay.isLeftDelay;

//                    ChargingTriggered(isDelay);
//                }
//                else if (KeyManager.instance.IsStayKeyDown("attack_right"))
//                {
//                    bool isDelay = false;
//                    relativeHand = HAND.RIGHT;
//                    isDelay = _delay.isRightDelay;

//                    ChargingTriggered(isDelay);
//                }

//                if (_isChargingStart)
//                {
//                    if (relativeHand == HAND.LEFT)
//                        attackKey = "attack_left";
//                    else if (relativeHand == HAND.RIGHT)
//                        attackKey = "attack_right";
//                }
//            }
//        }
//        else
//        {
//            if (currentStack < maxStack)
//            {
//                _currentTime += IsterTimeManager.deltaTime;
//                if (_currentTime >= (_stackTime * (currentStack + 1)))
//                {
//                    _effect.EffectOn(currentStack);

//                    currentStack++;
//                    if (currentStack < maxStack)
//                        _currentTime = 0.0f;
//                    else
//                        _currentTime = totalTime;
//                }
//            }

//            speed = 0.0f;

//            if (!KeyManager.instance.IsStayKeyDown(attackKey))
//            {
//                if (currentStack < 1)
//                    Attack();
//                else
//                    ChargeAttack();
//            }
//        }

//        _anim.AttackCharging(_isChargingStart);
//        _move.speed = speed;
//    }

//    public void EffectOn(bool isOn)
//    {
//        if (_effect)
//        {
//            if (!isOn)
//                _effect.EffectOff();
//        }
//    }

//    public void Attack()
//    {
//        ChargeCancle(true);

//        _attack.AttackTriggered(ATTACK_TYPE.NORMAL, relativeHand);
//        _attack.AttackStart();
//    }

//    public void ChargeAttack()
//    {
//        ChargeCancle(true);

//        _anim.ChargeAttack();

//        GameObject newBullet = CreateObject();

//        Vector3 playerPos = _move.center;
//        Vector3 dir = _look.dir;
//        dir.z = 0.0f;

//        BoxCollider2D collider = newBullet.GetComponentInChildren<BoxCollider2D>();
//        float distance = collider.size.x;
//        float stackScaleFactor = 0.3f * (currentStack - 1);
//        distance *= (collider.transform.localScale.x + stackScaleFactor);

//        float scaleFactor = PlayerReposition(playerPos, dir, distance);
//        Vector3 scale = newBullet.transform.localScale;
//        scale.x *= scaleFactor;
//        scale.y = (1.0f + stackScaleFactor);
//        newBullet.transform.localScale = scale;
//        distance *= scaleFactor;

//        float angle = CommonFuncs.DirToDegree(dir);
//        newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

//        newBullet.transform.position = playerPos + (dir * distance / 2.0f);

//        Damager damager = newBullet.GetComponentInChildren<Damager>();
//        Damage realDamage = damager.damage;
//        realDamage.damageMultiplier = (currentStack + 1);
//        damager.damage = realDamage;
//    }

//    float PlayerReposition(Vector3 playerPos, Vector3 dir, float distance)
//    {
//        float realDistance = distance;
//        RaycastHit2D[] hits = Physics2D.RaycastAll(playerPos, dir, distance, _obstacles);
//        foreach (var hit in hits)
//        {
//            if (hit.collider.isTrigger) continue;

//            if (hit.distance < realDistance)
//                realDistance = hit.distance;
//        }

//        Vector3 playerTargetPos = playerPos + dir * (realDistance - 0.5f);

//        _move.Move(playerTargetPos);

//        float scaleFactor = realDistance / distance;
//        return scaleFactor;
//    }

//    public void ChargeCancle(bool isEffectOff = false)
//    {
//        _isChargingStart = false;
//        _move.speed = _prevSpeed;

//        if (isEffectOff)
//            EffectOn(false);
//    }

//    void ChargingTriggered(bool isDelay)
//    {
//        if (!_skill.isSkillUsing)
//        {
//            if (!isDelay)
//                UseSkill();
//        }
//    }

//    public override void UseSkill()
//    {
//        EffectOn(true);

//        _isChargingStart = true;
//        _currentTime = 0.0f;

//        currentStack = 0;
//    }

//    public GameObject CreateObject()
//    {
//        GameObject newBullet = Instantiate(effectPrefab);

//        return newBullet;
//    }
//}
