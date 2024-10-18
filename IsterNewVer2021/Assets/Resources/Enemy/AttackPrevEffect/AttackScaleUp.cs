using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScaleUp : AttackPrevEffect
{
    protected Vector3 _Scale;
    // Start is called before the first frame update
    [SerializeField]
    ParticleSystem _effect;
    [SerializeField]
    bool _activefalseSelf;
    protected override void OnEnable()
    {
        _Scale = transform.localScale;
        base.OnEnable();

    }
    protected override IEnumerator DamagerOn()
    {
        float attackScale = transform.localScale.x;
        attackScale = Mathf.Lerp(0.0f, _Scale.x, 0.0f);

        while (_currentCount < _delayCount)
        {
            _currentCount += IsterTimeManager.deltaTime;

            float ratio = _currentCount / (_delayCount);

            attackScale = Mathf.Lerp(0.0f, _Scale.x, ratio);

            transform.localScale = new Vector3(attackScale, attackScale, attackScale);

            yield return null;
        }

        if (_damager)
        {
            _damager.SetActive(true);
            if (_effect) _effect.Play();
        }

        if(_coroutine != null)  StopCoroutine(_coroutine);
        if (_activefalseSelf) this.gameObject.SetActive(false);

    }
}
