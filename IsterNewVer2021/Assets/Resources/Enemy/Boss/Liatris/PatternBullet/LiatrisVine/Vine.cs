using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vine : MonoBehaviour
{
    //[SerializeField]
    //GameObject _stem;
    //
    //[SerializeField]
    //GameObject _root;
    [SerializeField]
    public BossController _controller;
    [SerializeField]
    public BossDamagableCondition _phaseChager;

    public GameObject _damageArea;

    public GameObject _damager;

    [HideInInspector]
    public Transform _prevPos;
    [HideInInspector]
    public Transform _attackPos;

    [SerializeField]
    SFXPlayer _sfx;

    public float _phase2Multiple;

   // bool _isAwake;
    //public bool isAwake { get { return _isAwake; } set { _isAwake = value; } }

    bool _isActive;
    public bool isActive { get { return _isActive; } set { _isActive = value; } }

    float _attackDelay;
    public float attackDelay { get { return _attackDelay; } set { _attackDelay = value; } }

    float _currentTimer;

    float _damagerOffTimer;

    [HideInInspector]
    public bool _isAttacking;
    [HideInInspector]
    public bool _isAttackStart;

    Coroutine _coroutine;

    [SerializeField]
    private ParticleSystem _effect;

    // Start is called before the first frame update
    private void OnEnable()
    {
        _currentTimer = 0;

        _damagerOffTimer = 0.4f;
        //_isAwake = false;
        _isActive = true;
        _effect.Stop();
        _damageArea.GetComponent<VineDamageArea>()._delayCount = _attackDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (_phaseChager.isPhaseChanging) { 
            _currentTimer = 0;
            if(_coroutine != null) StopCoroutine(_coroutine);

            _isAttacking = false;
        }
    }

    public void AttackStart()
    {
        _coroutine = StartCoroutine(ResetAttack());
        _damager.SetActive(true);

        if (_sfx) _sfx.PlaySFX("VineAttack");

        if (_effect)
            _effect.Play();
    }

    public void AttackEnd()
    {
        _currentTimer = 0;
        _isAttacking = false;
        _damager.SetActive(false);

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
    IEnumerator ResetAttack()
    {
        while (_currentTimer < _attackDelay)
        {
            if (_controller._isPhaseChage)
            {
                _currentTimer += (IsterTimeManager.bossDeltaTime * _phase2Multiple);
            }
            else _currentTimer += IsterTimeManager.bossDeltaTime;

            if(_damagerOffTimer < _currentTimer)
            {
                _damager.SetActive(false);
            }
            yield return null;
        }
        //public void DamageAreaOn()
        //{
        //    _damager.SetActive(true);
        //    if (_attackEffect) _attackEffect.Play();
        //}
        //public void DamageAreaOff()
        //{
        //    _damager.SetActive(false);
        //}
        AttackEnd();

    }
}
