using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowUpAndDown : GlowEffectBase
{
    [SerializeField]
    protected float _minIntensity = 0.3f;
    [SerializeField]
    protected float _maxIntensity = 1.0f;
    [SerializeField]
    protected float _frequency;

    private float _currentTime;
    private bool _isUp;

    public float minIntensity { get { return _minIntensity; } set { _minIntensity = value; } }
    public float maxIntensity { get { return _maxIntensity; } set { _maxIntensity = value; } }
    public float frequency { get { return _frequency; } set { _frequency = value; } }

    public float ratio { get; set; } = 1.0f;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        _currentTime = 0.0f;
        _isUp = true;
    }

    // Update is called once per frame
    void Update()
    {
        float start = 0.0f;
        float end = 0.0f;
        if (_isUp)
        {
            start = _minIntensity;
            end = _maxIntensity;
        }
        else
        {
            start = _maxIntensity;
            end = _minIntensity;
        }

        _currentTime += IsterTimeManager.enemyDeltaTime * _frequency;
        float percent = _currentTime / 1.0f;
        if (percent > 1.0f)
        {
            percent = 1.0f;
            _currentTime = 0.0f;
            _isUp = !_isUp;
        }
        
        float intensity = Mathf.Lerp(start, end, percent) * ratio;
        _glowObject.SetIntensity(intensity);
    }
}
