using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangUlYeePizza : TempBangUlYeePattern
{
    [SerializeField]
    List<GameObject> _pizzaCollider = new List<GameObject>();

    [SerializeField]
    ConditionalDoorTalkFrom _door;

    public float _holdConut;
    float _currentHoldCount;
    public bool _holding;

    public int _attackCount;

    public float _attackDelay;

    int _currentCount;

    float _currentTime;

    Coroutine _coroutine;
    Coroutine _holdingCoroutine;

    bool _patternStart;

    int _ranCount;

    private DebuffInfo _playerDebuff;

    public float prevSlowDecrease { get; set; }

    [SerializeField]
    private GameObject[] _pizzas;
    
    private void Start()
    {
        _patternStart = false;

        if (_holdingEffect)
            _holdingEffect.Stop();
    }
    public void PatternOn()
    {
        _patternStart = true;
        _ranCount = Random.Range(0, 2);
        _currentHoldCount = 0;
        _currentCount = 0;
        _currentTime = 0;
        _coroutine = StartCoroutine(PizzaActive());
    }
    public void PatternOff()
    {
        _patternStart = false;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }
    public void Update()
    {
        if (!_door.GetComponent<Collider2D>().isActiveAndEnabled && GetComponent<BangUlYeeAttacker>().IsInRange())
        {
            if (!_patternStart)
            {
                _currentTime += IsterTimeManager.enemyDeltaTime;
                if (_attackDelay < _currentTime)
                {
                    PatternOn();
                }
            }

            if (_holding)
            {
                if (_holdingCoroutine != null)
                    StopCoroutine(_holdingCoroutine);

                _holdingCoroutine = StartCoroutine(HoldEnd());
            }
        }
    }

    public override void PatternEnd()
    {
        _patternStart = false;

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

        foreach (var p in _pizzas)
            p.SetActive(false);
    }
    private bool _isPlayerHolding;
    [SerializeField]
    private ParticleSystem _holdingEffect;
    IEnumerator HoldEnd()
    {
        _currentHoldCount = 0.0f;
        if (!_playerDebuff)
            _playerDebuff = FindObjectOfType<PlayerMoveController>().GetComponent<DebuffInfo>();

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
                Vector3 newPos = _playerDebuff.transform.position;
                newPos.y += (-1.18f);
                _holdingEffect.transform.position = newPos;
            }

            _currentHoldCount += IsterTimeManager.enemyDeltaTime;
            yield return null;
        }

        PlayerSlow(prevSlowDecrease);

        if (_holdingEffect)
            _holdingEffect.Stop();

        _holdingCoroutine = null;

        _isPlayerHolding = false;
    }
    IEnumerator PizzaActive()
    {
        while (_currentCount < _attackCount)
        {
            while (_currentTime < _attackDelay)
            {
                _currentTime += IsterTimeManager.enemyDeltaTime;
                yield return null;
            }

            PiazzOn(_currentCount);

            _currentTime = 0;
            _currentCount++;

            yield return null;
        }

        while (_currentTime < _attackDelay)
        {
            _currentTime += IsterTimeManager.enemyDeltaTime;
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

    public void PlayerSlow(float figure)
    {
        if (!_playerDebuff)
            _playerDebuff = FindObjectOfType<PlayerMoveController>().GetComponent<DebuffInfo>();
        _playerDebuff.slowDecrease = figure;
    }
}
