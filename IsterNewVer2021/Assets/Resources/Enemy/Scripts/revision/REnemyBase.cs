using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum ENEMYTYPE
{
    NORMAL,
    MULTIPLE,
    NAMED,
    BOSS,
}

public abstract class REnemyBase : MonoBehaviour
{
    public GameObject Target = null;

    public REnemyController _controller;

    //플레이어 인식.
    [HideInInspector]
    public bool _playerCheck;
    public RFindingPlayer _findingPlayer;

    //에너미 시야.
    [HideInInspector]
    public bool _intoSight;
    public REnemySightController _enemySight;

    //에너미 Hp
    [HideInInspector]
    public Damagable _damagable;

    [HideInInspector]
    public NavMeshAgent _agent;

    //에너미 타입.
    public ENEMYTYPE _enemyType;
    
    public float _hp;
    public float _damage;
    public float _moveSpeed;
    public float _attackDelay;

    //인식 범위
    public float _checkRange;
    //원거리 공격 범위.
    public float _attackRange;
    public float attackRange { get { return _attackRange; } set { _attackRange = value; } }

    //근거리 공격 범위.
    public float _meleeAttackRange;
    public float meleeAttackRange { get { return _meleeAttackRange; } set { _meleeAttackRange = value; } }

    // Start is called before the first frame update

    public abstract void Init();
    void Start()
    {
        _controller = GetComponent<REnemyController>();
        _findingPlayer = GetComponentInChildren<RFindingPlayer>();
        _enemySight = GetComponentInChildren<REnemySightController>();
        _damagable = GetComponent<Damagable>();
        _agent = GetComponent<NavMeshAgent>();

        if (_damagable)
        {
            _damagable.totalHP = _hp;
            _damagable.currentHP = _hp;
        }
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetEnemy(REnemyBase enemy)
    {
        _controller._owner = enemy;
        if(_findingPlayer)
        _findingPlayer._owner = enemy;
        if(_enemySight)
        _enemySight._owner = enemy;
    }
}
