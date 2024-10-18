using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BangUlYeeAttacker : Attacker
{
    private PlayerMoveController _player;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D _rigid;
    //[SerializeField]
    //private NavMeshAgent _agent;
    [SerializeField]
    private BangUlYeeSight _sight;
    [SerializeField]
    private BangUlYeeMovable _move;
    [SerializeField]
    private Damagable _damagable;
    [SerializeField]
    private BangUlYeeBulletCreator _creator;

    [SerializeField]
    private EnemyFindingPlayer _finding;

    [SerializeField]
    private bool _isIgnorePlayerBuff = false;

    [Header("공격에 필요한 수치들")]
    [Tooltip("이 거리 안에 플레이어 있으면 공격")]
    [SerializeField]
    private float _attackRange;
    public float attackRange
    {
        get
        {
            float attackRange = _attackRange;

            if (!_isIgnorePlayerBuff)
            {
                if (!_playerBuff)
                    _playerBuff = FindObjectOfType<PlayerMoveController>().GetComponent<BuffInfo>();

                if (_playerBuff)
                    attackRange *= _playerBuff.enemyVisibleRatio;
            }

            return attackRange;
        }
    }
    private BuffInfo _playerBuff;
    
    [Tooltip("공격 쿨타임")]
    [SerializeField]
    private float _attackCoolTime = 5.0f;
    [SerializeField]
    private float _currentCoolTime;
    public bool isAttackReloaded { get { return (_currentCoolTime >= _attackCoolTime); } }
        
    private Vector2 _startDir;
    public Vector2 startDir { get { return _startDir; } }

    //불릿 크리에이터로 교체
    // [Header("이펙트")]
    // [SerializeField]
    // private GameObject _bullet;
    // [SerializeField]
    // private ParticleSystem _effect;
    // [SerializeField]
    // private ParticleSystem _readyEffect;

    [SerializeField]
    private SFXPlayer _sfx;

    private Animator _anim;

    private void OnEnable()
    {
        StartCoroutine(CoolTime());
    }
    void Start()
    {
        _player = FindObjectOfType<PlayerMoveController>();

        _anim = GetComponentInChildren<Animator>();
    }

    public override void CreateBullet()
    {
        _creator.FireBullets();
    }

    public override bool IsTriggered()
    {
        if (!_player) return false;
        if (_damagable.isDie || isAttacking || !_finding._findingPlayer) return false;

        bool isCanAttack = isAttackReloaded && IsInRange();

        return isCanAttack;
    }

    public bool IsInRange()
    {
        return Vector2.Distance(_player.center, transform.position) < attackRange;
    }

    public override void AttackStart()
    {
        base.AttackStart();

        if (_sfx)
            _sfx.PlaySFX("attack");


        _creator.Reload();
        
        _currentCoolTime = 0.0f;        
    }
  
    public override void AttackEnd()
    {
        base.AttackEnd();
       
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

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void OnHit()
    {
        _isIgnorePlayerBuff = true;
    }

    public void AttackEffector()
    {
        if (_attackEffector)
            _attackEffector.StartAttackEffect();
    }
}