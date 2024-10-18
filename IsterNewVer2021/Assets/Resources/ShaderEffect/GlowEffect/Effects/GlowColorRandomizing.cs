using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowColorRandomizing : GlowEffectBase
{
    [SerializeField]
    private float _frequency;

    private float _currentTime;

    private Color _startColor;
    private Color _endColor;

    protected override void Start()
    {
        base.Start();

        _currentTime = 0.0f;
        
        float r, g, b;
        r = Random.Range(0.0f, 1.0f);
        g = Random.Range(0.0f, 1.0f);
        b = Random.Range(0.0f, 1.0f);

        _startColor = new Color(r, g, b, 1.0f);

        r = Random.Range(0.0f, 1.0f);
        g = Random.Range(0.0f, 1.0f);
        b = Random.Range(0.0f, 1.0f);

        _endColor = new Color(r, g, b, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        _currentTime += IsterTimeManager.enemyDeltaTime * _frequency;
        float percent = _currentTime / 1.0f;
        if (percent > 1.0f)
        {
            percent = 0.0f;
            _currentTime = 0.0f;
            _startColor = _endColor;

            float r, g, b;
            r = Random.Range(0.0f, 1.0f);
            g = Random.Range(0.0f, 1.0f);
            b = Random.Range(0.0f, 1.0f);

            _endColor = new Color(r, g, b, 1.0f);
        }

        color = Color.Lerp(_startColor, _endColor, percent);
    }
}
