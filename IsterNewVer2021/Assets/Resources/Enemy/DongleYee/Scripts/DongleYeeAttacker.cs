using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DongleYeeAttacker : Attacker
{
    private PlayerMoveController _player;

    [Header("Components")]
    [SerializeField]
    private Rigidbody2D _rigid;
    [SerializeField]
    private NavMeshAgent _agent;
    [SerializeField]
    private DongleYeeSight _sight;
    [SerializeField]
    private DongleYeeMovable _move;
    [SerializeField]
    private Damagable _damagable;
    [SerializeField]
    private DongleYeeBulletCreator _creator;

    [Header("공격에 필요한 수치들")]
    [Tooltip("이 거리 안에 플레이어 있으면 공격")]
    [SerializeField]
    private float _attackRange = 5.0f;
    public float attackRange { get { return _attackRange; } }
    [Tooltip("공격 쿨타임")]
    [SerializeField]
    private float _attackCoolTime = 5.0f;
    private float _currentCoolTime;
    public bool isAttackReloaded { get { return (_currentCoolTime >= _attackCoolTime); } }

    private Vector2 _startDir;
    public Vector2 startDir { get { return _startDir; } }

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
        _startDir = CommonFuncs.CalcDir(transform, _player);
        _rigid.velocity = _startDir * 0;
    }

    public override bool IsTriggered()
    {
        if (!_move.player) return false;
        if (_damagable.isDie || isAttacking) return false;

        bool isCanAttack = isAttackReloaded && IsInRange();

        return isCanAttack;
    }

    private bool IsInRange()
    {
        return Vector2.Distance(_player.center, transform.position) < _attackRange;
    }

    public override void AttackStart()
    {
        base.AttackStart();
        //_move.enabled = false;
        _creator.Reload();
        //애니메이션에서 처리 or 
        //CreateBullet();

        _currentCoolTime = 0.0f;
    }

    public void Stop()
    {
        _move.speed = 0;
    }

    public override void AttackEnd()
    {
        base.AttackEnd();

        //_move.enabled = true;
        _move.SpeedReset();

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

    public void AttackEffect()
    {
        _attackEffector.StartAttackEffect();
    }
}
