using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldPizzaFlower : MonoBehaviour
{
    [SerializeField]
    FieldLiatrisController _liatris;

    [SerializeField]
    LiatrisFlowers _controller;

    [SerializeField]
    List<GameObject> _pizzaCollider = new List<GameObject>();

    public float _holdConut;
    float _currentHoldCount;
    public bool _holding;

    public float _attackDelay;

    float _currentTime;
    bool _pizzaOn;
    Coroutine _holdingCoroutine;

    public float prevSlowDecrease { get; set; }

    [SerializeField]
    private ParticleSystem _holdingEffect;

    private bool _isPlayerHolding;

    private DebuffInfo _playerDebuff;

    // Start is called before the first frame update
    void Start()
    {
        _currentTime = 0;
        if (_holdingEffect)
            _holdingEffect.Stop();
        _pizzaOn = false;
        _currentHoldCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_controller._damagable.isDie)
        {
            if (_controller._isSpawned)
            {
                if (_attackDelay > _currentTime)
                {
                    _currentTime += IsterTimeManager.bossDeltaTime;
                    if (_currentTime > _attackDelay)
                    {
                        _pizzaOn = true;
                        _currentTime = 0;
                    }                
                }
                if(_pizzaOn) PizzaOn();

                if (_holding && !_isPlayerHolding)
                {
                    _holding = false;

                    if (_holdingCoroutine != null)
                        StopCoroutine(_holdingCoroutine);

                    _holdingCoroutine = StartCoroutine(HoldEnd());
                }
            }
            else return;
        }
        else PatternEnd();
    }
    void PatternEnd()
    {
        if (_holdingCoroutine != null)
        {
            StopCoroutine(_holdingCoroutine);

            if (_holdingEffect)
                _holdingEffect.Stop();
        }

        if (_isPlayerHolding)
            PlayerSlow(prevSlowDecrease);

        PizzaOff();
    }

    void PizzaOn()
    {
        int _pizzaNum;
        _pizzaNum = Random.Range(0, _pizzaCollider.Count);
        if (_pizzaCollider.Count != 0 && _pizzaOn)
        {
            //_pizzaNum = Random.Range(0, _pizzaCollider.Count);
            if(_pizzaCollider[_pizzaNum].activeSelf)
            {
                PizzaOn();
            }

            _pizzaCollider[_pizzaNum].SetActive(true); 
            _pizzaOn = false;           
        }
    }
    void PizzaOff()
    {
        for (int i = 0; i < _pizzaCollider.Count; i++)
        {
            if (_pizzaCollider[i].activeSelf)
            {
                _pizzaCollider[i].SetActive(false);
            }
        }
    }
    IEnumerator HoldEnd()
    {
        _currentHoldCount = 0;
        _isPlayerHolding = true;

        if (_holdingEffect)
        {
            _holdingEffect.Play();
        }


        while (_currentHoldCount < _holdConut)
        {
            if (_holdingEffect.isPlaying)
            {
                Vector3 newPos = _liatris.player.transform.position;
                newPos.y += (-1.18f);
                _holdingEffect.transform.position = newPos;
            }

            yield return null;

            _currentHoldCount += IsterTimeManager.bossDeltaTime;
        }

        PlayerSlow(prevSlowDecrease);

        if (_holdingEffect.isPlaying)
                _holdingEffect.Stop();

       // _holdingCoroutine = null;
       
        _isPlayerHolding = false;

        _currentHoldCount = 0;

        StopCoroutine(_holdingCoroutine);
    }
    public void PlayerSlow(float figure)
    {
        if (!_playerDebuff)
            _playerDebuff = _liatris.player.GetComponent<DebuffInfo>();
        _playerDebuff.slowDecrease = figure;
    }
}
