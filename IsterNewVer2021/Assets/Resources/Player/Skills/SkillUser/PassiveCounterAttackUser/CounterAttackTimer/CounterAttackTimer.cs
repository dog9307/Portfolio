using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackTimer : MonoBehaviour
{
    private PassiveCounterAttackUser _user;

    [SerializeField]
    private float MAX_TIME = 0.5f;
    private float _currentTime;

    public bool isCanCounter
    {
        get
        {
            return (_user != null) && (_currentTime < MAX_TIME);
        }

        set
        {
            if (!value)
            {
                _currentTime = MAX_TIME;

                if (_effect)
                    _effect.enabled = false;
            }
        }
    }

    private SpriteRenderer _effect;

    void Start()
    {
        _user = GetComponentInParent<PassiveCounterAttackUser>();

        _effect = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime > MAX_TIME) return;

        _currentTime += IsterTimeManager.enemyDeltaTime;
        if (_currentTime > MAX_TIME)
        {
            if (_effect)
                _effect.enabled = false;
        }
    }
    
    public void TimerStart()
    {
        if (!_user) return;

        _currentTime = 0.0f;

        if (_effect)
            _effect.enabled = true;
    }
}
