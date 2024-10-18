using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TymeSpawnLock : BossAttackBase
{
    public float _holdConut;
    float _currentHoldCount;

    public bool _holding;

    public float _attackDelay;

    float _currentTime;

    Coroutine _coroutine;
    Coroutine _holdingCoroutine;

    public float prevSlowDecrease { get; set; }

    [SerializeField]
    private bool _isPlayerHolding;

    private DebuffInfo _playerDebuff;

    [SerializeField]
    private ParticleSystem _effector;

    public override void SetPatternId()
    {
        if (_holdingEffect)
            _holdingEffect.Stop();
    }
    // Start is called before the first frame update
    public override void PatternOn()
    {
        _currentHoldCount = 0;
        _currentTime = 0;
        _coroutine = StartCoroutine(PizzaActive());
    }
    public override void PatternOff()
    {
        _owner.attackPatternEnd = true;
        StopCoroutine(_coroutine);
    }

    public override void PatternEnd()
    {
        base.PatternEnd();
        LockOff();
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        if (_holdingCoroutine != null)
        {
            StopCoroutine(_holdingCoroutine);

            if (_holdingEffect)
                _holdingEffect.Stop();
        }

        if (_isPlayerHolding)
            PlayerSlow(prevSlowDecrease);
    }
    public override void Update()
    {
        base.Update();

        if (_holding && !_isPlayerHolding)
        {
            if (_holdingCoroutine != null)
                StopCoroutine(_holdingCoroutine);

            _holdingCoroutine = StartCoroutine(HoldEnd());
        }

    }
    [SerializeField]
    private ParticleSystem _holdingEffect;
    IEnumerator HoldEnd()
    {
        _isPlayerHolding = true;
        if (_holdingEffect)
        {
            if (_holdingEffect.isPlaying)
                _holdingEffect.Stop();

            _holdingEffect.Play();
        }

        _holding = false;

        while (_currentHoldCount < _holdConut)
        {
            if (_holdingEffect)
            {
                Vector3 newPos = _owner.player.transform.position;
                newPos.y += (-1.18f);
                _holdingEffect.transform.position = newPos;
            }

            yield return null;

            _currentHoldCount += IsterTimeManager.bossDeltaTime;
        }

        PlayerSlow(prevSlowDecrease);

        if (_holdingEffect)
            _holdingEffect.Stop();

        _isPlayerHolding = false;
        _currentHoldCount = 0;
        StopCoroutine(_holdingCoroutine);
    }
    IEnumerator PizzaActive()
    {
        _attacker._attackStart = false;
       
         while (_currentTime < _attackDelay)
         {
             _currentTime += IsterTimeManager.bossDeltaTime;
             yield return null;
         }

         LockOn();

        _currentTime = 0;

        yield return null;
    }
    void LockOn()
    {
        GameObject newBullet = CreateObject();
        newBullet.transform.position = _owner.player.transform.transform.position;

        if (_effector)
        {
            ParticleSystem effector = Instantiate<ParticleSystem>(_effector);

            effector.transform.position = newBullet.transform.position;
            effector.Play();
        }
    }
    void LockOff()
    {
        PatternOff();
    }


    public void PlayerSlow(float figure)
    {
        if (!_playerDebuff)
            _playerDebuff = _owner.player.GetComponent<DebuffInfo>();
        _playerDebuff.slowDecrease = figure;
    }

}