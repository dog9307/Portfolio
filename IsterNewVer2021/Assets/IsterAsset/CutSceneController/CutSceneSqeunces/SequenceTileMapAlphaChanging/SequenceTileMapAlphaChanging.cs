using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SequenceTileMapAlphaChanging : CutSceneSqeunceBase
{
    [Header("Å¸ÀÏ¸Ê ¾ËÆÄ")]
    [SerializeField]
    private Tilemap _relativeTilemap;
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
        Color color = _relativeTilemap.color;
        color.a = alpha;
        _relativeTilemap.color = color;
    }
}
