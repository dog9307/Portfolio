using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossController : BossControllerBase
{
    //기본 컴포넌트들
    [SerializeField]
    private BossHPBarController _hpBar;

    [SerializeField] 
    protected BossMain _bossMain;

    [SerializeField]
    protected BossAttacker _attacker;

    [SerializeField]
    protected BossMovable _movable;
    //타깃
    [SerializeField]
    PlayerMoveController _player;
    public PlayerMoveController player { get { return _player; } }
    //보스 활성화
    bool _isActive;
    public bool isActive { get { return _isActive; } set { _isActive = value; } }

    //페이즈 관리
    public bool _isPhaseChage;

    //에너미가 안보이게 될 때.
    public bool _isHide;

    //패턴 관리할 부분
    //public bool _patternStart;
    public bool _isCanPattern;
    public float _patternDelay;
    
    protected Queue<BossPatternBase> _patternSquence = new Queue<BossPatternBase>();
    protected BossPatternBase currentSequence; //그로기때 종료해야함

    private bool _attackPatternStart;
    public bool attackPatternStart { get { return _attackPatternStart; } set { _attackPatternStart = value; } }
    private bool _attackPatternEnd;
    public bool attackPatternEnd { get { return _attackPatternEnd; } set { _attackPatternEnd = value; } }
    private bool _movePatternStart;
    public bool movePatternStart { get { return _movePatternStart; } set { _movePatternStart = value; } }
    private bool _movePatternEnd;
    public bool movePatternEnd { get { return _movePatternEnd; } set { _movePatternEnd = value; } }

    [SerializeField]
    protected bool _inMelee;
    public bool inMelee {get { return _inMelee; }set { _inMelee = value; } }

    [HideInInspector]
    public bool _isMeleeAttack;

    public bool _grogi;
    [HideInInspector]
    public bool _grogiTimeEnd;

    public float _grogiResetTimer;
    float _currentGrogiTimer;
    //코루틴 관리
    protected Coroutine _currentCoroutine;


    // Start is called before the first frame update
    private void OnEnable()
    {
        if(!_player)
            _player = FindObjectOfType<PlayerMoveController>();
    }
    protected virtual void Start()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();
        _currentGrogiTimer = 0;
        _inMelee = false;
        _isHide = false;
        isActive = false;
        _isPhaseChage = false;
        _isMeleeAttack = false;
        _isCanPattern = true;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.F7)) BattleStart();
        if (!_bossMain.damagable.isDie)
        {
            if (_grogi && !_grogiTimeEnd)
            {
                if(_isCanPattern) PatternReset();

                if(_grogiResetTimer > _currentGrogiTimer)
                {
                    _currentGrogiTimer += IsterTimeManager.bossDeltaTime;
                    if (_grogiResetTimer < _currentGrogiTimer)
                    {
                        _currentGrogiTimer = 0;
                        _grogiTimeEnd = true;
                    }
                }
            }

            if (_isHide || !isActive || !_isCanPattern)
                _bossMain.damagable.isCanHurt = false;
            else _bossMain.damagable.isCanHurt = true;
        }
        else GetComponent<BossAniCotroller>().Die();
    }

    public UnityEvent OnBattleStart;
    public void BattleStart()
    {
        PatternReset();
        isActive = true;

        if (_hpBar)
            _hpBar.StartBattle();

        if (OnBattleStart != null)
            OnBattleStart.Invoke();
    }

    public virtual void GrogiReset()
    {
        _grogi = false;
        _grogiTimeEnd = false;

        PatternReset();
    }


    public void PatternReset()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _attacker.AttackReset();
        _movable.MoveReset();
        _attackPatternStart = false;
        _attackPatternEnd = false;
        _movePatternStart = false;
        _movePatternEnd = false;
        _isMeleeAttack = false;
        
        if (!_grogi) PatternSetter();
    }

    public virtual void GrogiEnter()
    {
        if (currentSequence)
        {
            currentSequence.PatternEnd();
            currentSequence = null;
        }

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _attacker.AttackReset();
        _movable.MoveReset();

        _attackPatternStart = false;
        _attackPatternEnd = false;
        _movePatternStart = false;
        _movePatternEnd = false;
        _isCanPattern = false;
        _grogi = true;
    }

    protected virtual void PatternSetter()
    {
        currentSequence = null;
        _patternSquence.Clear();
    }

    public IEnumerator PatternStart()
    {
        if(_grogi) PatternReset();

        yield return new WaitForSeconds(_patternDelay);

        if (_patternSquence.Count > 0)
        {
            currentSequence = _patternSquence.Dequeue();

            while (currentSequence)
            {
                currentSequence.PatternStart();

                while (currentSequence.isPatternEnd)
                {
                    currentSequence = _patternSquence.Dequeue();
                    currentSequence.PatternStart();
                }

                while (!currentSequence.isPatternEnd)
                {
                    yield return null;
                }

                if (_patternSquence.Count > 0)
                    currentSequence = _patternSquence.Dequeue();
                else
                    currentSequence = null;
            }
        }

        yield return new WaitForSeconds(_patternDelay);

        _currentCoroutine = null;
        PatternReset();
    }

    public override void BossWakeUp()
    {
        _isActive = true;
    }

    public UnityEvent OnDie;
    public override void BossDie()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer)
        {
            renderer.sortingLayerName = "Foreground";
            renderer.sortingOrder = BlackMaskController.BLACKMASK_ORDER_IN_LAYER + 1;
        }

        if (OnDie != null)
            OnDie.Invoke();
    }

    public override void BossDisappear()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer)
            renderer.sortingLayerName = "Dynamic";
    }
}
