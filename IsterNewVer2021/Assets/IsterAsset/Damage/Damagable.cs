using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damagable : MonoBehaviour
{
    [SerializeField] private float _totalHP;
    public float totalHP { get { return _totalHP; } set { _totalHP = value; if (_currentHP > _totalHP) _currentHP = _totalHP; } }
    
    [SerializeField]
    private float _currentHP;
    public float currentHP { get { return _currentHP; } set { _currentHP = value; } }

    private float _extraCurrentHP;
    public float extraCurrentHP { get { return _extraCurrentHP; } set { _extraCurrentHP = value; } }

    private float _extraTotalHP;
    public float extraTotalHP { get { return _extraTotalHP; } set { _extraTotalHP = value; } }

    private Queue<Damage> _damages = new Queue<Damage>();
    
    public bool isHurt { get { return (_damages.Count != 0); } }
    public bool isDie { get { return _currentHP <= 0.0f; } }
    public virtual bool isCanHurt{ get; set; }
    public bool isKnockback { get; set; }

    private Rigidbody2D _rigid;

    private DamageMarker _marker;
    public DamageMarker marker { get { return _marker; } set { _marker = value; } }

    private PlayerMoveController _player;
    private PassiveCoolTimeDownUser _passiveCoolTime;
    public const float PLAYER_KNOCKBACK_FIGURE = 25.0f;

    private BuffInfo _buff;

    [SerializeField]
    private DamagableStateResetter _resetter;

    public bool isRelativeDirection { get; set; }

    [SerializeField]
    private bool _isDieEffectOn = true;

    public UnityEvent OnHit;

    [SerializeField]
    private GameObject _dieEffectPrefab;

    private SFXPlayer _sfx;

    public Vector3 center
    {
        get
        {
            Collider2D collider = GetComponent<Collider2D>();
            if (!collider)
                return transform.position;

            return collider.bounds.center;
        }
    }

    [SerializeField]
    private float _cameraShakingFigure = 5.0f;
    public float cameraShakingFigure { get { return _cameraShakingFigure; } set { _cameraShakingFigure = value; } }

    [SerializeField]
    private AnimController _anim;

    public bool isAleardyHitted { get; set; }


    private static GameObject _enemyEffectPrefab;
    private EnemyHitEffector _effector;
    [SerializeField]
    private bool _isEffectorOn = true;

    [SerializeField]
    private DamagableCondition _condition;

    public UnityEvent OnDie;

    void Awake()
    {
        _currentHP = _totalHP;
    }

    void Start()
    {
        Recovery();

        _rigid = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerMoveController>();
        if (_player)
        {
            totalHP = PlayerPrefs.GetFloat("PlayerTotalHP", 60.0f);
            Recovery();
            //_passiveCoolTime = _player.GetComponentInChildren<SkillUserManager>().FindUser(typeof(PassiveCoolTimeDown)) as PassiveCoolTimeDownUser;
        }

        _buff = GetComponent<BuffInfo>();

        isAleardyHitted = false;

        if (!_player)
        {
            if (!_isEffectorOn) return;

            if (!_enemyEffectPrefab)
                _enemyEffectPrefab = Resources.Load<GameObject>("Damage/HitEffect/EnemyHitEffector/EnemyHitEffector");

            if (!_enemyEffectPrefab) return;

            _effector = GetComponentInChildren<EnemyHitEffector>();
            if (_effector) return;

            GameObject newEffect = Instantiate(_enemyEffectPrefab);
            newEffect.transform.parent = transform;
            newEffect.transform.localPosition = Vector3.zero;
            newEffect.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            _effector = newEffect.GetComponent<EnemyHitEffector>();
            if (_effector)
            {
                SpriteRenderer relativeRenderer = GetComponent<SpriteRenderer>();
                if (!relativeRenderer)
                    relativeRenderer = GetComponentInChildren<SpriteRenderer>();

                _effector.parentRenderer = relativeRenderer;
            }
        }
    }

    // test
    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Alpha1))
    //    {
    //        if (GetComponent<PlayerMoveController>())
    //        {
    //            LookAtMouse look = GetComponent<LookAtMouse>();
    //            if (look)
    //            {
    //                Damage damage = DamageCreator.Create(gameObject, currentHP, 0.0f, 1.0f);
    //                HitDamager(damage, -look.dir);
    //            }
    //        }

    //        FindObjectOfType<PlayerHPUIController>().HPChange();

    //        CameraShakeController.instance.CameraShake(_cameraShakingFigure);

    //        if (GetComponent<PlayerMoveController>())
    //            TotalHPUp(20.0f);
    //    }
    //}

    // test
    public void ResetState()
    {
        if (!_resetter) return;

        _resetter.StateReset();
    }

    public void Recovery()
    {
        _currentHP = _totalHP;

        isCanHurt = true;
        isKnockback = false;
    }

    void LateUpdate()
    {
        if (!isHurt) return;

        float totalDamage = 0.0f;
        bool isMarkerDestroy = false;
        for (int i = 0; i < _damages.Count;)
        {
            if (_player)
                isCanHurt = false;

            Damage damage = _damages.Dequeue();
            float damageFigure = damage.realDamage;
            if (damageFigure < 0.0f)
            {
                _currentHP -= damageFigure;
                if (_currentHP > totalHP)
                    _currentHP = totalHP;

                continue;
            }

            if (_buff)
                damageFigure -= _buff.familiarDamageDecrease;

            DebuffInfo debuffInfo = GetComponent<DebuffInfo>();
            if (debuffInfo)
                damageFigure += (damageFigure * debuffInfo.getMoreDamage);

            if (damageFigure > 0.0f)
            {
                if (!isMarkerDestroy)
                    isMarkerDestroy = damage.isMarkerDestroy;
            }

            totalDamage += (isTutorial ? 0.0f : damageFigure);
            CalcHP((isTutorial ? 0.0f : damageFigure));

            if (isDie)
            {
                if (!_anim)
                    _anim = GetComponent<AnimController>();

                if (_anim)
                    _anim.Die();

                if (!_player)
                {
                    ActiveTimeSlowUser timeSlow = FindObjectOfType<ActiveTimeSlowUser>();
                    if (timeSlow)
                        timeSlow.TimeUpByKill();

                    if (_isDieEffectOn)
                    {
                        GameObject prefab = _dieEffectPrefab;
                        if (!prefab)
                            prefab = Resources.Load<GameObject>("Enemy/EnemyDieEffect");

                        if (prefab)
                        {
                            GameObject dieEffcect = Instantiate(prefab);
                            dieEffcect.transform.parent = transform;
                            dieEffcect.transform.localPosition = Vector3.zero;
                        }
                    }
                }

                if (OnDie != null)
                    OnDie.Invoke();

                break;
            }
        }

        CameraShakeController.instance.CameraShake(_cameraShakingFigure);

        if (totalDamage > 0.0f)
        {
            HitEffectOn();

            if (_passiveCoolTime)
            {
                if (_passiveCoolTime.isDefeatCoolTime)
                {
                    _passiveCoolTime.startPos = transform.position;
                    _passiveCoolTime.DefeatCoolTimeDown();
                }
            }
            
            if (_marker)
            {
                if (isMarkerDestroy)
                {
                    _marker.triggeredDamageFigure = totalDamage;
                    _marker.DestroyMarker();
                    _marker = null;
                }
            }
            else
                _marker = GetComponentInChildren<DamageMarker>();
        }
    }

    void CalcHP(float damage)
    {
        if (_extraCurrentHP > 0.0f)
        {
            _extraCurrentHP -= damage;

            if (_extraCurrentHP < 0.0f)
            {
                damage = -_extraCurrentHP;
                _extraCurrentHP = 0.0f;
            }
            else
                damage = 0.0f;
        }

        float prevHP = _currentHP;
        _currentHP -= damage;
        if (_currentHP <= 0.0f)
            _currentHP = 0.0f;

        if (_currentHP < prevHP)
            isAleardyHitted = true;
    }

    // 넉백 상황 정리
    // 데미지 수치와 넉백 수치는 따로 관리
    // 데미지는 있지만 넉백이 없을 수 있다
    // 넉백이 조금이라도 있으면 State는 리셋된다
    public void Knockback(float figure, Vector2 dir)
    {
        if (!_rigid) return;
        //if (figure <= 0.1f) return;

        Vector2 force = dir * figure;
        if (force.magnitude < float.Epsilon) return;

        ResetState();

        isKnockback = true;
        if (_player)
            figure = PLAYER_KNOCKBACK_FIGURE;
        _rigid.AddForce(force, ForceMode2D.Impulse);
    }

    [SerializeField]
    private GameObject _hitEffect;

    [SerializeField]
    private string[] _onHitEffectNames;

    public void HitEffectOn()
    {
        if (_effector)
            _effector.StartHitEffect();

        if (_onHitEffectNames == null) return;
        if (_onHitEffectNames.Length <= 0) return;

        int index = Random.Range(0, _onHitEffectNames.Length);

        HitEffectManager.instance.StartEffect(_onHitEffectNames[index]);
    }

    public bool isTutorial { get; set; } = false;
    public virtual void HitDamager(Damage damage, Vector2 dir)
    {
        if (_condition)
        {
            if (!_condition.IsCanHitted())
                return;
        }

        if (!isCanHurt || isDie) return;
        if (!IsCorrectDirection(damage, dir)) return;

        _damages.Enqueue(damage);

        Knockback(damage.knockbackFigure, dir);

        GameObject hitEffect = _hitEffect;
        if (!hitEffect)
            hitEffect = Resources.Load<GameObject>("misc/HitEffect/normal_hitted");

        if (hitEffect)
        {
            GameObject effect = Instantiate(hitEffect);
            effect.transform.position = transform.position;
        }

        if (OnHit != null)
            OnHit.Invoke();

        if (!_sfx)
            _sfx = GetComponentInChildren<SFXPlayer>();
        if (_sfx)
            _sfx.PlaySFX("hit");
    }

    private bool IsCorrectDirection(Damage damage, Vector2 bulletToObjDir)
    {
        if (!isRelativeDirection) return true;
        if (!damage.owner) return true;

        Vector2 objDir = Vector2.zero;

        LookAtMouse look = GetComponent<LookAtMouse>();
        if (look)
            objDir = look.dir;
        else
            objDir = _rigid.velocity.normalized;

        float dot = Vector2.Dot(bulletToObjDir, objDir);

        return (dot >= 0);
    }

    private PlayerHPUIController _uiCon;
    public void Heal(float heal)
    {
        currentHP += heal;
        if (currentHP > totalHP)
            currentHP = totalHP;

        UIUpdate();
    }

    public void TotalHPUp(float hp)
    {
        totalHP += hp;
        Heal(hp);

        UIUpdate();
    }

    public void ExtraHeal(float heal)
    {
        extraCurrentHP += heal;
        if (extraCurrentHP > extraTotalHP)
            extraCurrentHP = extraTotalHP;

        UIUpdate();
    }

    public void AddExtraHP(float hp)
    {
        _extraTotalHP += hp;
        _extraCurrentHP += hp;

        UIUpdate();
    }

    public void RemoveExtraHP(float hp)
    {
        _extraTotalHP -= hp;
        if (_extraTotalHP < 0.0f)
            _extraTotalHP = 0.0f;

        _extraCurrentHP = (_extraCurrentHP < _extraTotalHP ? _extraCurrentHP : _extraTotalHP);

        UIUpdate();
    }

    void UIUpdate()
    {
        if (!_player)
        {
            _player = GetComponent<PlayerMoveController>();
            if (!_player) return;
        }

        if (!_uiCon)
            _uiCon = GetComponent<PlayerHPUIController>();

        _uiCon.HPChange();
    }
}
