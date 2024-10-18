using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Damagable))]
public class Undamagable : MonoBehaviour
{
    [SerializeField] private float _totalTime;
    [SerializeField] private float _duration;
    private float _currentTime;
    private float _currentTotalTime;

    private float _alpha;

    private SpriteRenderer _sprite;

    public bool isUndamagable
    {
        get { return (_currentTotalTime <= _totalTime); }
        set
        {
            if (value)
                UndamagableStart();
            else
                _currentTotalTime = _totalTime;
        }
    }

    private Damagable _damagable;

    void UndamagableStart()
    {
        _currentTime = 0.0f;
        _currentTotalTime = 0.0f;

        _alpha = 0.0f;
    }

    void Start()
    {
        _currentTotalTime = _totalTime;

        _sprite = GetComponent<SpriteRenderer>();
        _damagable = GetComponent<Damagable>();
    }

    void Update()
    {
        if (!isUndamagable) return;

        _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= _duration)
        {
            _currentTime = 0.0f;

            _alpha = (_alpha == 0.0f ? 1.0f : 0.0f);
            _sprite.color = new Color(1.0f, 1.0f, 1.0f, _alpha);

            _currentTotalTime += _duration;
            if (_currentTotalTime >= _totalTime)
            {
                //EventTimer timer = GetComponent<EventTimer>();
                //if (!timer)
                    _damagable.isCanHurt = true;

                _sprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
        }
    }

    public void StopUndamagable()
    {
        isUndamagable = false;
        _sprite.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }
}
