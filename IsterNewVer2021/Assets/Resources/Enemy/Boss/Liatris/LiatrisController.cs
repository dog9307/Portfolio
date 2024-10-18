using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiatrisController : BossController
{

   // [SerializeField]
   // List<Animator> _animator = new List<Animator>();
    [SerializeField]
    LiatrisSphereController _shpereCon;
    [SerializeField]
    WallCreatorSpawnPattern _wallCreator;
    public FlowerType type { get; set; }

    [SerializeField]
    //private EnemySpawnPattern _spawn;

    [HideInInspector]
    public bool _phaseChange;

    public bool _isSword;

    [SerializeField]
    GameObject _ChangeAnimatorObject;

    [SerializeField]
    float _phase2StartDelay;

    Coroutine _coroutine;

    [SerializeField]
    SFXPlayer _sfx;

    protected override void Start()
    {
        base.Start();

        _isSword = true;
    }
    protected override void Update()
    {
        base.Update();

        if (_bossMain.damagable.currentHP < _bossMain.damagable.totalHP / 2 && !_isPhaseChage)
        {
            GetComponent<BossAniCotroller>().PhaseChange();
        }
        //
        // if (_isPhaseChage && !_tracing._isFire)
        // {
        //     if(_currentBulletTimer < _tracingBulletTimer)
        //     {
        //         _currentBulletTimer += IsterTimeManager.bossDeltaTime;
        //     }
        //     else
        //     {
        //         _currentBulletTimer = 0;
        //         _shpereCon.TracingSetter();
        //     }
        // }

        SwapSwordAnimation();
    }
    protected override void PatternSetter()
    {
        base.PatternSetter();

        int patternNum;

        if (_attacker.attackList.Count == 0 && _attacker.attackSecondList.Count != 0)
        {
            GetComponent<LiatrisAttacker>().ListChange();
        }


        if (inMelee)
        {
            _patternDelay = 0.5f;
            _isMeleeAttack = true;
            if (_attacker.meleeAttackList.Count != 0)
            {
                patternNum = Random.Range(0, _attacker.meleeAttackList.Count);
                _shpereCon.SphereSetter(_attacker.meleeAttackList[patternNum].patternID);
                _patternSquence.Enqueue(_attacker.meleeAttackList[patternNum]);
            }
        }
        else
        {
            _patternDelay = 2.0f;

            if (_attacker.attackList.Count != 0)
            {
                patternNum = Random.Range(0, _attacker.attackList.Count);
                _patternSquence.Enqueue(_attacker.attackList[patternNum]);
                //_shpereCon.SphereSetter(_attacker.attackList[patternNum].patternID);
                _shpereCon.SphereSetter(_attacker.attackList[patternNum].patternID);
                //_shpereCon.TracingSetter();
                _attacker.attackSecondList.Add(_attacker.attackList[patternNum]);
                _attacker.attackList.Remove(_attacker.attackList[patternNum]);
            }
        }

        if (_movable.moveList.Count != 0)
        {
            if (!_isPhaseChage)
            {
                _patternDelay = 0.5f;
                _patternSquence.Enqueue(_movable.moveList[0]);
            }
            else
            {
                _patternDelay = 0.5f;
                _patternSquence.Enqueue(_movable.moveList[1]);
            }
        }

        _currentCoroutine = StartCoroutine(PatternStart());
    }
    [SerializeField]
    private LiatrisSpecialPattern _special;
    public override void GrogiEnter()
    {
        CameraShakeController.instance.CameraShake(15.0f);

        _shpereCon.SphereReset();

        transform.localScale = new Vector3(1, 1, 1);

        //if (_special)
        //    _special.DisappearPattern();

        base.GrogiEnter();

        //_spawn.BossDie();

       // if (_tracing && _tracing.gameObject.activeSelf)
       //     _tracing.BossGrogi(_grogiResetTimer);
    }

    public override void GrogiReset()
    {
        base.GrogiReset();
        _isCanPattern = true;
        _wallCreator._spawnStart = false;
    }

    public void ReturnToPhase()
    {
        _coroutine = StartCoroutine(ResetTime());
    }
    IEnumerator ResetTime()
    {
        yield return new WaitForSeconds(_phase2StartDelay);

        GrogiReset();
        StopCoroutine(_coroutine);
        _coroutine = null;
    }

   //[SerializeField]
   //private LiatrisPlayerTracyingBullet _tracing;
    public override void BossDie()
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
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
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        if (renderer)
            renderer.sortingLayerName = "Dynamic";
    }

    public void ExchangeAnimator()
    {
        Animator _ani;
        _ani = _ChangeAnimatorObject.GetComponent<Animator>();

        _ChangeAnimatorObject.SetActive(true);
        _ani.Play("NMask_sword_phasechange");
        if (_sfx) _sfx.PlaySFX("mask_off");
        GetComponent<BossAniCotroller>()._anim = _ani;
    }

    public void SwapSwordAnimation()
    {
        GetComponent<BossAniCotroller>()._anim.SetBool("isSword", _isSword);

        if (_isSword) { 
        }
        else
        {
        }
    }
}
