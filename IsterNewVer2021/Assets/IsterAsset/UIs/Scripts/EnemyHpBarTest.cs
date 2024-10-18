using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBarTest : MonoBehaviour
{
    [SerializeField]
    private Damagable _enemyDamagable;

    [SerializeField]
    private Image _front;
    [SerializeField]
    private Image _back;

    private bool _onHpbar;
    private bool _timerStart;
    public float _hpTimer;
    private float _currentTimer;
   
    private Graphic[] _graphics;

    void Start()
    {
        _graphics = GetComponentsInChildren<Graphic>();
        ApplyAlpha(0.0f);
        _front.fillAmount = 0.0f;
        _currentTimer = 0.0f ;
        _timerStart = false;
    }

    void LateUpdate()
    {
        if (!_enemyDamagable.isDie)
        {
            if (_timerStart)
            {
                _currentTimer += IsterTimeManager.deltaTime;

                if (_currentTimer > _hpTimer)
                {
                    _currentTimer = 0;
                    _timerStart = false;

                    ApplyAlpha(0);
                }
            }

            float ratio = _enemyDamagable.currentHP / _enemyDamagable.totalHP;
            _front.fillAmount = ratio;

            _back.fillAmount = Mathf.Lerp(_back.fillAmount, _front.fillAmount, 0.1f);

            if (_back.fillAmount < _front.fillAmount)
                _back.fillAmount = _front.fillAmount;
        }
        else
        {
            ApplyAlpha(0);
        }
    }

    public void ApplyAlpha(float alpha)
    {
        _timerStart = true;
        _currentTimer = 0;

        foreach (var g in _graphics)
        {
            Color color = g.color;
            color.a = alpha;
            g.color = color;
        }
    }   
}