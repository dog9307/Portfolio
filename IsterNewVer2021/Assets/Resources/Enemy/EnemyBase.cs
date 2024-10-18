using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{

    public GameObject Target = null;

    //protected EnemyMovable _movable;
    //protected EnemyAttacker _attacker;
    protected FindingPlayer _findingPlayer;
   // protected EnemySightController _enemySight;
    //protected EnemyController _enemyController;
    //protected EnemyMoveController _enemyMoveController;

    //-----------------------------------플레이어 인식 판별------------------------------------------
    //플레이어 인식했는가
    [SerializeField]
    protected bool _isPlayerCheck = false;
    public bool isPlayerCheck { get { return _isPlayerCheck; } set { _isPlayerCheck = value; } }
    //시야 범위에 들어왔는가
    protected bool _isIntoSight = false;
    public bool isIntoSight { get { return _isIntoSight; } set { _isIntoSight = value; } }
    //인식 가능한 범위 ----원거리 몬스터는 인식 범위까지 도망친다----if(!isMeleeMonster)
    [SerializeField]
    protected float _checkingRange;
    public float checkingRange { get { return _checkingRange; } set { _checkingRange = value; } }

    //----------------------------------------------------------------------------------------------

    //-----------------------------------몬스터 기본 설정--------------------------------------------
    //목표가 있는 몬스터인가
    protected bool _isTargetting = true;
    public bool isTargetting { get { return _isTargetting; } set { _isTargetting = value; } }

    //근접 몬스터인가
    protected bool _isMeleeMoster;
    public bool isMeleeMoster { get { return _isMeleeMoster; } set { _isMeleeMoster = value; } }
    //공격 범위   ----원거리 몬스터는 attackRange가 checkingRange 보다 길어야함----
    [SerializeField]
    protected float _attackRange;
    public float attackRange { get { return _attackRange; } set { _attackRange = value; } }
    //공격 범위 들어 왔는가
    [SerializeField]
    protected bool _isIntoRange;
    public bool isIntoRange { get { return _isIntoRange; } set { _isIntoRange = value; } }
    //공격 가능한 상태인가(시야각 안에 있고, 공격범위 안에 있는가.)
    protected bool _isAttackAble;
    public bool isAttackAble { get { return _isAttackAble; } set { _isAttackAble = value; } }
    //공격 중인가.
    protected bool _isAttacking;
    public bool isAttacking { get { return _isAttacking; } set { _isAttacking = value; } }
    //-------------------------------------몬스터 특수 설정-------------------------------------------- 
    //-------------------------------------대쉬 몬스터-------------------------------------------------
    //대쉬형 공격 몬스터.
    protected bool _isDashMonster;
    public bool isDashMonster { get { return _isDashMonster; } set { _isDashMonster = value; } }
    //대쉬 최대 거리
    protected float _dashRange;
    public float dashRange { get { return _dashRange; } set { _dashRange = value; } }
    //대쉬 공격 중인가
    protected bool _isDash;
    public bool isDash { get { return _isDash; } set { _isDash = value; } }
    //------------------------------------------------------------------------------------------------
    //------------------------------------------------------------------------------------------------

    protected abstract void Init();
    protected abstract void StateControll();

    void Start()
    { 
        //_attacker = GetComponent<EnemyAttacker>();
        //_movable = GetComponent<EnemyMovable>();
       // _enemyController = GetComponent<EnemyController>();
        _findingPlayer = GetComponentInChildren<FindingPlayer>();
       // _enemySight = GetComponentInChildren<EnemySightController>();

        Init();
    }
    void Update()
    {
        StateControll();
    }
    public void SetEnemy(EnemyBase enemy)
    {
       // _enemyController._owner = enemy;
        _findingPlayer._enemyBase = enemy; 
       // _enemySight._enemyBase = enemy;
        //_enemyMoveController._enemybase = enemy;
    }
}
