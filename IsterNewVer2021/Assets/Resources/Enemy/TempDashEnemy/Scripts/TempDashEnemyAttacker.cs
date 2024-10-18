using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TempDashEnemyAttacker : Attacker
{
    private PlayerMoveController _player;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D _rigid;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private TempDashEnemySight _sight;
    [SerializeField]
    private TempDashEnemyMovable _move;
    [SerializeField]
    private Damagable _damagable;

    [Header("공격에 필요한 수치들")]
    [Tooltip("이 거리 안에 플레이어 있으면 공격")]
    [SerializeField]
    private float _attackRange = 5.0f;

    [Tooltip("대시중 속도")]
    [SerializeField]
    private float _attackSpeed = 12.0f;

    [Tooltip("공격 쿨타임")]
    [SerializeField]
    private float _attackCoolTime = 5.0f;
    private float _currentCoolTime;
    public bool isAttackReloaded { get { return (_currentCoolTime >= _attackCoolTime); } }

    [Tooltip("공격 유지 시간")]
    [SerializeField]
    private float _attackTime = 1.5f;

    [Tooltip("선딜")]
    [SerializeField]
    private float _attackReadyTime = 2.0f;
    private float _currentReadyTime = 0.0f;
    [HideInInspector]
    public bool _isReady;

    private Vector2 _startDir;
    public Vector2 attackVelocity { get { return _startDir * _attackSpeed; } }

    [Header("이펙트")]
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private ParticleSystem _effect;
    [SerializeField]
    private ParticleSystem _readyEffect;

    private PhysicsMaterial2D _pMat;

    private Animator _anim;

    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();

        _anim = GetComponent<Animator>();

        _currentCoolTime = _attackCoolTime;
        _pMat = _rigid.sharedMaterial;
        _isReady = false;
    }

    public override void CreateBullet()
    {
        _bullet.SetActive(true);
        _startDir = CommonFuncs.CalcDir(transform, _player);
        _rigid.velocity = _startDir * _attackSpeed;
        _rigid.sharedMaterial = null;
    }

    public override bool IsTriggered()
    {
        if (!_move.player) return false;
        if (_damagable.isDie || isAttacking) return false;

        bool isCanAttack = isAttackReloaded && IsInRange();
        if (isCanAttack)
        {
            _isReady = true;
            _currentReadyTime += IsterTimeManager.enemyDeltaTime;

            if (_readyEffect)
            {
                if (!_readyEffect.isPlaying)
                    _readyEffect.Play();
            }
        }
        else
        {
            _currentReadyTime = 0.0f;

            if (_readyEffect)
            {
                if (_readyEffect.isPlaying)
                    _readyEffect.Stop();
            }
        }

        return isCanAttack && (_currentReadyTime >= _attackReadyTime);
    }

    private bool IsInRange()
    {
        return Vector2.Distance(_player.center, transform.position) < _attackRange;
    }

    public override void AttackStart()
    {
        base.AttackStart();

        //_move.enabled = false;
        _agent.enabled = false;

        CreateBullet();

        _effect.Play();
        if (_readyEffect)
        {
            if (_readyEffect.isPlaying)
                _readyEffect.Stop();
        }

        _currentCoolTime = 0.0f;

        gameObject.layer = LayerMask.NameToLayer("EnemyGhost");

        StartCoroutine(Attacking());
    }

    public void Stop()
    {
        _rigid.velocity = Vector2.zero;
    }

    public override void AttackEnd()
    {
        base.AttackEnd();

        //_move.enabled = true;
        _agent.enabled = true;
        _isReady = false;
        _effect.Stop();

        _bullet.SetActive(false);

        gameObject.layer = LayerMask.NameToLayer("Enemys");

        _rigid.sharedMaterial = _pMat;

        StartCoroutine(CoolTime());
    }

    IEnumerator CoolTime()
    {
        while (_currentCoolTime < _attackCoolTime)
        {
            yield return null;

            _currentCoolTime += IsterTimeManager.enemyDeltaTime;
        }
        _currentCoolTime = _attackCoolTime;
    }

    IEnumerator Attacking()
    {
        float currentTime = 0.0f;
        float totalTime = _attackTime;
        while (isAttacking)
        {
            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
            if (currentTime >= totalTime)
            {
                _anim.SetTrigger("attackEnd");
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
