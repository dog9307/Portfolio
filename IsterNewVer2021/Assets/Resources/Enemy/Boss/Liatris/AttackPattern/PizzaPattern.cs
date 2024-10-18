using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaPattern : BossAttackBase
{
    [SerializeField]
    List<GameObject> _pizzaCollider =new List<GameObject>();

    public float _holdConut;
    float _currentHoldCount;
    public bool _holding;

    public int _attackCount;

    public float _attackDelay;

    int _currentCount;

    float _currentTime;

    Coroutine _coroutine;
    Coroutine _holdingCoroutine;

    int _ranCount;

    public float prevSlowDecrease { get; set; }

    [SerializeField]
    private bool _isPlayerHolding;

    private DebuffInfo _playerDebuff;

    public override void SetPatternId()
    {
        _patternID = 103;

        if (_holdingEffect)
            _holdingEffect.Stop();
    }
    // Start is called before the first frame update
    public override void PatternOn()
    {
        _ranCount = Random.Range(0, 2);
        _currentHoldCount = 0;
        _currentCount = 0;
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
        PiazzOff();
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

        while (_currentCount < _attackCount)
        {
            while (_currentTime < _attackDelay)
            {
                _currentTime += IsterTimeManager.bossDeltaTime;
                yield return null;
            }

            PiazzOn(_currentCount);

            _currentTime = 0;
            _currentCount++;

            yield return null;
        }

        while (_currentTime < _attackDelay)
        {
            _currentTime += IsterTimeManager.bossDeltaTime;
            yield return null;
        }

        PatternOff();
    }
    void PiazzOn(int count)
    {
        for (int i = 0; i < _pizzaCollider.Count; i++)
        {
            if (_ranCount % 2 == 0)
            {
                if (count % 2 == 0)
                {
                    if (i % 2 == 0) _pizzaCollider[i].gameObject.SetActive(true);
                    else continue;
                }
                else
                {
                    if (i % 2 != 0) _pizzaCollider[i].gameObject.SetActive(true);
                    else continue;
                }
            }
            else
            {
                if (count % 2 == 0)
                {
                    if (i % 2 != 0) _pizzaCollider[i].gameObject.SetActive(true);
                    else continue;
                }
                else
                {
                    if (i % 2 == 0) _pizzaCollider[i].gameObject.SetActive(true);
                    else continue;
                }
            }
        }
    }
    void PiazzOff()
    {
        for (int i = 0; i < _pizzaCollider.Count; i++)
        {
            if (_pizzaCollider[i].activeSelf) _pizzaCollider[i].SetActive(false);
            else continue;
        }
    }
    public void PlayerSlow(float figure)
    {
        if (!_playerDebuff)
            _playerDebuff = _owner.player.GetComponent<DebuffInfo>();
        _playerDebuff.slowDecrease = figure;
    }
}
