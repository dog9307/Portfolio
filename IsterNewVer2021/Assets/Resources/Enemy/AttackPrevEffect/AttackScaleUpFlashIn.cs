using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScaleUpFlashIn : AttackPrevEffect
{
    [ColorUsage(true, true)]
    [SerializeField] private Color _flashColor;
    [SerializeField] AnimationCurve _flashSpeedCurve;

    protected Vector3 _Scale;

    public float _flashCount;
    float _currentflashCount;

    [SerializeField]
    SpriteRenderer _spriteRenderer;

    [SerializeField] GameObject _materialOB;
    Material _material;

    [SerializeField] ParticleSystem _effect;

    [SerializeField]
    bool _activefalseSelf;
    protected override void OnEnable()
    {
        _Scale = transform.localScale;

        transform.localScale = new Vector3(0, 0, 0);
        _currentflashCount = 0;
        _material = _spriteRenderer.material;
        _coroutine = StartCoroutine(DamagerOn());
    }
    // Start is called before the first frame update
    protected override IEnumerator DamagerOn()
    {
        float attackScale = _Scale.x;
        attackScale = Mathf.Lerp(0.0f, _Scale.x, 0.0f);

        while (_currentCount < _delayCount)
        {
            _currentCount += IsterTimeManager.deltaTime;
            
            float ratio = _currentCount / _delayCount;

            attackScale = Mathf.Lerp(0.0f, _Scale.x, ratio);

            transform.localScale = new Vector3(attackScale, attackScale, attackScale);

            yield return null;
        }

        _coroutine = StartCoroutine(FlashOn());
    }
    IEnumerator FlashOn()
    {
        SetFlashColor();

        float currentFlashAmount = 0;
        _currentflashCount = 0;
        while (_currentflashCount < _flashCount)
        {
            _currentflashCount += IsterTimeManager.deltaTime;

            float ratio = _currentflashCount / _flashCount;

            currentFlashAmount = Mathf.Lerp(0.0f, _flashSpeedCurve.Evaluate(_currentflashCount), ratio);

            SetFlashAmount(currentFlashAmount);

            yield return null;
        }

        if (_damager)
        {
            if (_effect)
            {
                if (_effect.gameObject.activeSelf) _effect.Play();
                else
                {
                    _effect.gameObject.SetActive(true);
                    _effect.Play();
                }
            }

            _damager.SetActive(true);
        }

        StopCoroutine(_coroutine);

        if (_activefalseSelf) this.gameObject.SetActive(false);
    }

    void SetFlashColor()
    {
        _material.SetColor("_FlashColor", _flashColor);
    }
    void SetFlashAmount(float amount)
    {
        _material.SetFloat("_FlashAmount", amount);
    }
}