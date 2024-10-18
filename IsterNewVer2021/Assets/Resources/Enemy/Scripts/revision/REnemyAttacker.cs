using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REnemyAttacker : MonoBehaviour
{
    public REnemyController _controller;
    
    public ENEMYTYPE _type;
    [HideInInspector]

    public bool _attackStart;
    public bool _attackEnd;
   
    public RangeAttackSetter _rangeAttackSetter = new RangeAttackSetter();
    public MeleeAttackSetter _meleeAttackSetter = new MeleeAttackSetter();

    [HideInInspector]
    public bool _isRepeat;

    public float _timer;
    public float _intervalCount;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (_controller)
        {
            if (_controller.attackpattern != null)
            {
                for (int i = 0; i < _controller.attackpattern.Count; i++)
                {
                    _controller.attackpattern[i].Reload();
                }
            }
        }

        _intervalCount = 0;
        _timer = 0;
        if (_rangeAttackSetter._repeatCount > 0 || _meleeAttackSetter._repeatCount > 0)
        {
            _isRepeat = true;
        }
        else _isRepeat = false;
        
    }
    void Start()
    {
        _controller = GetComponent<REnemyController>();

        _type = _controller._owner._enemyType;
    }

    // Update is called once per frame
    void Update()
    {
        if (_attackStart)
        {
            _timer += IsterTimeManager.enemyDeltaTime;

            if (_rangeAttackSetter._repeatCount > 0)
            {
                if (_timer > _rangeAttackSetter._intervalTimer)
                {
                    _timer = 0;
                    AttackFire();
                    if (_intervalCount > _rangeAttackSetter._repeatCount)
                    {
                        _attackStart = false;
                        _controller.AttackerOff();
                    }

                }
            }
            else if (_meleeAttackSetter._repeatCount > 0)
            {
                if (_timer > _rangeAttackSetter._intervalTimer)
                {
                    _timer = 0;
                    AttackFire();

                    if (_intervalCount > _rangeAttackSetter._repeatCount)
                    {
                        _attackStart = false;
                        _controller.AttackerOff();
                    }
                }
            }
        }

        if (_isRepeat)
        {
            if (_rangeAttackSetter._repeatCount < _intervalCount || _meleeAttackSetter._repeatCount < _intervalCount)
            {
                _attackStart = false;
                _controller.AttackerOff();
            }
        }

        //foreach (REnemyAttackPatternBase pattern in _controller.attackpattern)
        //{
        //    if (pattern._isShoot)
        //    {
        //        _controller.AttackerOff();
        //    } 
        //    else
        //    {
        //        AttackFire();
        //    }
        //}
    }
    public void AttackEnd()
    {
        _controller.AttackerOff();
    }
    public void AttackFire()
    {
        if (_isRepeat)
        {
            _intervalCount++;
        }

        switch (_type)
        {
            case ENEMYTYPE.NORMAL:
                if (_controller._owner.Target)
                {
                    if (_controller.attackpattern.Count != 0)
                    {
                        if (_controller.attackpattern[0]._isShoot)
                        {
                            _controller.AttackerOff();
                        }
                        else
                        {
                            _controller.attackpattern[0].FireBullet();
                        }
                    }
                }
                else _controller.AttackerOff();
                break;
            case ENEMYTYPE.MULTIPLE:
                if (_controller._owner.Target)
                {
                    if (_controller.attackpattern.Count != 0)
                    {
                        foreach (REnemyAttackPatternBase pattern in _controller.attackpattern)
                        {
                            if (CommonFuncs.Distance(this.transform.position, _controller._owner.Target.transform.position) < _controller._owner._meleeAttackRange)
                            {
                                if (pattern.GetType().Name.Contains("Melee") || pattern.GetType().Name.Contains("Dash"))
                                {
                                    if (pattern._isShoot)
                                    {
                                        _controller.AttackerOff();
                                    }
                                    else
                                    {
                                        pattern.FireBullet();
                                    }
                                }
                                else continue;
                            }
                            else if (CommonFuncs.Distance(this.transform.position, _controller._owner.Target.transform.position) < _controller._owner._attackRange && CommonFuncs.Distance(this.transform.position, _controller._owner.Target.transform.position) > _controller._owner._meleeAttackRange)
                            {
                                if (pattern.GetType().Name.Contains("Range"))
                                {
                                    if (pattern._isShoot)
                                    {
                                        _controller.AttackerOff();
                                    }
                                    else
                                    {
                                        pattern.FireBullet();
                                    }
                                }
                                else continue;
                            }
                            else _controller.AttackerOff();

                            break;
                        }
                    }
                }
                else _controller.AttackerOff();
                break;
            case ENEMYTYPE.NAMED:
                if (_controller._owner.Target)
                {
                    if (_controller.attackpattern.Count != 0)
                    {
                        foreach (REnemyAttackPatternBase pattern in _controller.attackpattern)
                        {
                            if (pattern.GetType().Name.Contains(this.gameObject.name))
                            {
                                if (pattern._isShoot)
                                {
                                    _controller.AttackerOff();
                                }
                                else
                                {
                                    pattern.FireBullet();
                                }
                            }     
                        }
                    }
                }
                else _controller.AttackerOff();
                break;
            case ENEMYTYPE.BOSS:
                if (_controller._owner.Target)
                {
                    if (_controller.attackpattern.Count != 0)
                    {
                        foreach (REnemyAttackPatternBase pattern in _controller.attackpattern)
                        {
                            if (pattern.GetType().Name.Contains(this.gameObject.name))
                            {
                                if (pattern._isShoot)
                                {
                                    _controller.AttackerOff();
                                }
                                else
                                {
                                    pattern.FireBullet();
                                }
                            }
                        }
                    }
                }
                break;
            default:
                 _controller.AttackerOff();
                break;
        }
    }

    private CanCounterAttackedObject _counter;
    public void CounterAttackStart()
    {
        if (!_counter)
            _counter = GetComponent<CanCounterAttackedObject>();
        if (!_counter) return;

        _counter.TimerStart();
    }


    public Vector2 attackDir { get; set; }
    private PlayerMoveController _player;
    public void AttackDirSetting()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        attackDir = CommonFuncs.CalcDir(this, _player);
    }
}


