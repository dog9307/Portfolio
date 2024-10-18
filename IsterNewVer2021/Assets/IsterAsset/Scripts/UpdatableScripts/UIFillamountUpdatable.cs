using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFillamountUpdatable : UpdatableComponent
{
    [SerializeField]
    private Image _cover;

    [SerializeField]
    private float _resetMultiplier = 10.0f;

    private Coroutine _resetCoroutine;

    public override void UpdateManually(float deltaTime)
    {
        if (_resetCoroutine != null)
        {
            StopCoroutine(_resetCoroutine);
            _resetCoroutine = null;
        }

        _cover.fillAmount = deltaTime;
    }

    public override void ResetUpdateManually()
    {
        _resetCoroutine = StartCoroutine(ResetRatio());
    }

    IEnumerator ResetRatio()
    {
        while (_cover.fillAmount > 0.0f)
        {
            float ratio = _cover.fillAmount - IsterTimeManager.originDeltaTime * _resetMultiplier;
            ratio = Mathf.Clamp(ratio, 0.0f, 1.0f);

            _cover.fillAmount = ratio;

            yield return null;
        }
        _cover.fillAmount = 0.0f;

        _resetCoroutine = null;
    }
}
