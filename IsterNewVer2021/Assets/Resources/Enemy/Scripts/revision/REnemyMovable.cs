using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MOVETYPE
{
    TRACE,
    FIX,
}
public class REnemyMovable : Movable
{
    public REnemyController _controller;

    protected float _moveSpeed;

    [HideInInspector]
    public Vector3 _dir;
    [HideInInspector]
    public Vector3 _dashDir;

    public MP _moveType;

    [HideInInspector]
    public bool _isDash;
    [HideInInspector]
    public bool _isDashEnd;

    [HideInInspector]
    public float _dashMutiple;


    [SerializeField]
    private Transform _idleMovePos;
    [SerializeField]
    private float _idleMoveTime = 2.0f;
    [SerializeField]
    private float _idleMoveRange = 5.0f;

    private Coroutine _idleMoveCoroutine;

    private BuffInfo _playerBuff;
    private PlayerMoveController _findingTarget;

    [SerializeField]
    private bool _isIgnorePlayerBuff = false;

    [SerializeField]
    private float _findingPlayerRange = 10.0f;
    public float findingPlayerRange
    {
        get
        {
            float findingRange = _findingPlayerRange;

            if (!_isIgnorePlayerBuff)
            {
                if (!_playerBuff)
                {
                    if (_findingTarget)
                        _playerBuff = _findingTarget.GetComponent<BuffInfo>();
                }

                if (_playerBuff)
                    findingRange *= _playerBuff.enemyVisibleRatio;
            }

            return findingRange;
        }
    }

    private PlayerMoveController _player;
    public PlayerMoveController player { get { return _player; } }

    private void OnDestroy()
    {
        if (_idleMoveCoroutine != null)
            StopCoroutine(_idleMoveCoroutine);
    }

    // Start is called before the first frame update  
    void Start()
    {
        _controller = GetComponent<REnemyController>();

        _dashMutiple = 1.5f;

        _moveSpeed = _controller._owner._moveSpeed;
    }
    [SerializeField]
    private NavMeshAgent _agent;
    protected override void ComputeVelocity()
    {
        if (_controller._owner._agent)
        {
            if (_controller._owner.meleeAttackRange > 0)
            {
                _controller._owner._agent.stoppingDistance = _controller._moveRange * 0.6f;
            }
            else
            {
                _controller._owner._agent.stoppingDistance = _controller._moveRange * 0.8f;
            }
        }

        if (!_player)
        {
            _agent.speed = speed * IsterTimeManager.enemyTimeScale;

            if (!_findingTarget)
                _findingTarget = FindObjectOfType<PlayerMoveController>();

            if (_findingTarget)
            {
                if (_idleMoveCoroutine == null)
                    _idleMoveCoroutine = StartCoroutine(IdleMove());

                if (CommonFuncs.Distance(_findingTarget.transform.position, transform.position) < findingPlayerRange)
                    _player = _findingTarget;
            }
        }
        else
        {
            if (_idleMoveCoroutine != null)
                StopCoroutine(_idleMoveCoroutine);

            if (_controller._owner.Target)
            {
                if (CommonFuncs.Distance(this.transform.position, _controller._owner.Target.transform.position) < _controller._moveRange)
                {
                    _controller._inRange = true;
                }
                else
                {
                    _controller._inRange = false;
                }
            }
        }

        //if(_moveType == MP.dash)
        //{
        //    if(_controller._inRange)
        //    {
        //        _isDash = true;
        //    }
        //}
        

        MoveStyle();
    }
    public void MoveStyle()
    {
        _dir = _controller._owner._enemySight.targetDir;

        //타켓이 있어
        if (_controller._owner.Target)
        {
            if (!_damagable.isHurt && !_damagable.isDie && !_damagable.isKnockback)
            {
                //사거리 밖
                switch (_moveType)
                {
                    case MP.tracking:
                        if (!_controller._inRange)
                        {
                            if (!_controller._attackOn)
                            {
                                _speed = _moveSpeed;
                                if (_controller._owner._agent)
                                {
                                    _controller._owner._agent.speed = _speed;
                                    _targetVelocity = _dir * _controller._owner._agent.speed;
                                }
                                else
                                {
                                    _targetVelocity = _dir * _speed;
                                }

                            }
                        }
                        else
                        {
                            _speed = 0;
                            _controller._owner._agent.speed = _speed;
                        }
                        //if (_controller._inRange)
                        //{
                        //    _controller.MovableOff();
                        //}
                        break;
                    case MP.teleport:

                        break;

                    case MP.dash:
                        if (_isDash)
                        {
                            _dir = _controller._owner._enemySight.virDir;
                            _moveSpeed = _controller._owner._moveSpeed * _dashMutiple;
                            _controller._owner._agent.speed = _moveSpeed;
                            //_controller._owner._agent.speed = _moveSpeed;
                            _targetVelocity = _dir * _moveSpeed;
                        }
                        else
                        {
                            if (!_controller._inRange)
                            {
                                if (!_controller._attackOn)
                                {
                                    _moveSpeed = _controller._owner._moveSpeed;

                                    if (_controller._owner._agent)
                                    {
                                        _controller._owner._agent.speed = _moveSpeed;
                                        _targetVelocity = _dir * _controller._owner._agent.speed;
                                    }
                                    else
                                    {
                                        _targetVelocity = _dir * _moveSpeed;
                                    }
                                }
                                else
                                {
                                    _moveSpeed = 0;
                                    _controller._owner._agent.speed = _moveSpeed;
                                }
                            }
                        }                    
                        break;
                    default:
                        break;
                }
                //_targetVelocity = _dir * _moveSpeed;                            
            }       
        }
        else
        {
            _dir = _controller._owner._enemySight.targetDir;

            switch (_moveType)
            {
                case MP.tracking:
                    {

                        if (_controller._owner._enemySight._currentTimer > _controller._idleCounter)
                        {
                            _moveSpeed = _controller._owner._moveSpeed * 0.4f;
                            _targetVelocity = _dir * _moveSpeed;
                        }
                     
                        break;
                    }
                case MP.teleport:
                    {
                        break;
                    }
                case MP.dash:
                    {
                        if (_controller._owner._enemySight._currentTimer > _controller._idleCounter)
                        {
                            _moveSpeed = _controller._owner._moveSpeed * 0.4f;
                            _targetVelocity = _dir * _moveSpeed;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }

    IEnumerator IdleMove()
    {
        while (true)
        {
            float x = Random.Range(-_idleMoveRange, _idleMoveRange);
            float y = Random.Range(-_idleMoveRange, _idleMoveRange);
            Vector3 offset = new Vector3(x, y, 0.0f);
            _idleMovePos.transform.position = transform.position + offset;

            if(_agent.pathPending)
                _agent.SetDestination(_idleMovePos.position);

            yield return new WaitForSeconds(_idleMoveTime);
        }
    }


    public void OnHit()
    {
        _player = FindObjectOfType<PlayerMoveController>();
    }
}
