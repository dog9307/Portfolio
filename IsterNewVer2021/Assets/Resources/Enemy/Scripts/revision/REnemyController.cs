using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class REnemyController : MonoBehaviour
{
    public REnemyBase _owner;

    //어태커 받기.
    protected REnemyAttacker _attacker;
    public REnemyAttacker attacker { get { return _attacker; }}
    //무버블 받기.
    protected REnemyMovable _movable;
    public REnemyMovable movable { get { return _movable; } }
      
    [HideInInspector]
    protected float _attackDelay;
    [SerializeReference]
    protected List<REnemyAttackPatternBase> _attackPattern;
    public List<REnemyAttackPatternBase> attackpattern { get { return _attackPattern; } set { _attackPattern = value; } }

    [SerializeReference]
    protected List<REnemyMovePatternBase> _movePattern;
    public List<REnemyMovePatternBase> movePattern { get { return _movePattern; } set { _movePattern = value; } }

    //공격중인가.
    [HideInInspector]
    public bool _attackOn;
    //이동중인가
    [HideInInspector]
    public bool _moveOn;

    //가동 범위
    [HideInInspector]
    public float _moveRange;
    //사거리 안에 있는가.
    //[HideInInspector]
    public bool _inRange;
    [HideInInspector]
    public float _delayCounter;

    //유휴시간 컨트롤러.(따로 빼거나 삭제예정)
    public float _idleCounter;

    protected float _resetTimer;

    [SerializeField]
    private EnemyAttackEffector _effector;

    //public virtual void Init()
    //{
    //    AttackRangeSetter();

    //    SetControllers(this);

    //    _attackDelay = _owner._attackDelay;
    //    _inRange = false;
    //}
    // Start is called before the first frame update
    private void OnEnable()
    {
    }
    protected void Start()
    {
        //Init();

        _attacker = GetComponent<REnemyAttacker>();
        _movable = GetComponent<REnemyMovable>();

        _delayCounter = 0;

        SetControllers(this);
        
        _inRange = false;
        
        AttackerOff();
    }

    // Update is called once per frame
    protected void Update()
    {
        AttackRangeSetter();

        if (!_owner._damagable.isHurt && !_owner._damagable.isDie)
        {
            if (!_attacker.isActiveAndEnabled && _inRange)
            {
                _delayCounter += IsterTimeManager.deltaTime;
                if (_delayCounter > _owner._attackDelay)
                {
                    ResetAttacker();
                    _delayCounter = 0;
                }
            }
        }
    }
    public void ResetAttacker()
    {       
        _attacker.enabled = true;
    }
    public void AttackerOff()
    {
        _attacker._attackEnd = true;
        _attacker.enabled = false;
    }
    //public void ResetMovable()
    //{
    //    _movable.enabled = true;
    //}
    //public void MovableOffOnly()
    //{
    //    if (_owner._agent)
    //    {
    //        _owner._agent.speed = 0;
    //    }

    //    _movable.enabled = false;
    //}
    //public void MovableOff()
    //{
    //    ResetAttacker();

    //    if (_owner._agent)
    //    {
    //        _owner._agent.speed = 0;
    //    }

    //    _movable.enabled = false;
    //}

    public void SetStateReset()
    {
        AttackerOff();
       // MovableOffOnly();
        _delayCounter = 0;
        //if (!_movable.isActiveAndEnabled && !_attacker.isActiveAndEnabled)
        //{
        //    _delayCounter += IsterTimeManager.deltaTime;
        //    if (_delayCounter > _owner._attackDelay)
        //    {
        //        ResetMovable();
        //        _delayCounter = 0;
        //    }
        //}
    }
    public void SetControllers(REnemyController controller)
    {
        if(_attacker) _attacker._controller = this;
        if(_movable) _movable._controller = this;
    }
    public void AttackRangeSetter()
    {
        if (_owner.meleeAttackRange > 0 && _owner.attackRange == 0)
        {
            _moveRange = _owner.meleeAttackRange * 0.9f;
        }
        else if (_owner.meleeAttackRange == 0 && _owner.attackRange > 0)
        {
            _moveRange = _owner.attackRange * 0.8f;
        }
        else if (_owner.meleeAttackRange > 0 && _owner.attackRange > 0)
        {
            _moveRange = (_owner.meleeAttackRange + _owner.attackRange) * 0.6f;           
        }
    }

    public void AttackEffector()
    {
        if (_effector)
            _effector.StartAttackEffect();
    }
}
