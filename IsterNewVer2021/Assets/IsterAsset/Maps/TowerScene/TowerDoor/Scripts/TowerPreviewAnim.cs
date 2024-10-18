using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPreviewAnim : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _renderer;

    [SerializeField]
    private float _minAlpha = 0.3f;
    [SerializeField]
    private float _maxAlpha = 1.0f;

    public float minAlpha { get { return _minAlpha; } set { _minAlpha = value; } }
    public float maxAlpha { get { return _maxAlpha; } set { _maxAlpha = value; } }

    [SerializeField]
    private float _alphaMultiplier;
    [SerializeField]
    private float _ratio;

    void Update()
    {
        Color color = _renderer.color;
        color.a = Mathf.Lerp(minAlpha * _alphaMultiplier, maxAlpha * _alphaMultiplier, _ratio);
        _renderer.color = color;
    }
}
