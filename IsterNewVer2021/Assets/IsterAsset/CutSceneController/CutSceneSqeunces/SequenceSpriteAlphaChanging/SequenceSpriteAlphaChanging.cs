using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceSpriteAlphaChanging : CutSceneSqeunceBase
{
    [Header("스프라이트 알파")]
    [SerializeField]
    private SpriteRenderer _relativeSprite;
    [SerializeField]
    private float _startAlpha = 0.0f;
    [SerializeField]
    private float _endAlpha = 1.0f;

    protected override IEnumerator DuringSequence()
    {
        ApplyAlpha(_startAlpha);

        float currentTime = 0.0f;
        while (currentTime < _sequenceTime)
        {
            float ratio = currentTime / _sequenceTime;
            float alpha = Mathf.Lerp(_startAlpha, _endAlpha, ratio);

            ApplyAlpha(alpha);

            yield return null;

            currentTime += IsterTimeManager.deltaTime;
        }
        ApplyAlpha(_endAlpha);

        _isDuringSequence = false;
    }

    public void ApplyAlpha(float alpha)
    {
        Color color = _relativeSprite.color;
        color.a = alpha;
        _relativeSprite.color = color;
    }
}
