using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorCircleChecker : AttackPrevEffect
{
    [SerializeField]
    CircleSectorShootPattern _circleSector;

    [SerializeField]
    private SpriteRenderer _renderer;
    private Material _mat;

    protected Vector3 _Scale;
    // Start is called before the first frame update
    protected override void OnEnable()
    {
        _Scale = transform.localScale;
        _currentCount = 0;
        if (!_circleSector)
        {
            _circleSector = FindObjectOfType<CircleSectorShootPattern>();
        }
        _circleSector._right.transform.localRotation = Quaternion.Euler(0, 0, _circleSector._angle / 2);
        _circleSector._left.transform.localRotation = Quaternion.Euler(0, 0, -_circleSector._angle / 2);

        base.OnEnable();

    }
    protected override IEnumerator DamagerOn()
    {
        float attackScale = transform.localScale.x;
        attackScale = Mathf.Lerp(0.0f, _Scale.x, 0.0f);

        while (_currentCount < _delayCount)
        {
            _currentCount += IsterTimeManager.deltaTime;

            float ratio = _currentCount / _delayCount;

            attackScale = Mathf.Lerp(0.0f, _Scale.x, ratio);

            transform.localScale = new Vector3(attackScale, attackScale, attackScale);

            yield return null;
        }
    
        if (_coroutine != null) StopCoroutine(_coroutine);
    }
}