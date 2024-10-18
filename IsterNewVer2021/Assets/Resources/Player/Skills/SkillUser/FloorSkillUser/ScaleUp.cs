using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUp : MonoBehaviour
{
    [SerializeField]
    private float _totalTime;
    private float _currentTime;

    public Vector2 targetScale { get; set; }

    [SerializeField]
    private ParticleSystem _effect;

    void OnEnable()
    {
        TimeReset();
    }

    public void TimeReset()
    {
        _currentTime = 0.0f;

        Vector3 scale;
        scale.x = 0.0f;
        scale.y = 0.0f;
        scale.z = 1.0f;
        transform.localScale = scale;

        if (_effect)
            _effect.Stop();
    }

    void Update()
    {
        _currentTime += IsterTimeManager.enemyDeltaTime;
        if (_currentTime >= _totalTime)
            _currentTime = _totalTime;

        float percent = _currentTime / _totalTime;
        Vector3 scale;
        scale.x = Mathf.Lerp(0.0f, targetScale.x, percent);
        scale.y = Mathf.Lerp(0.0f, targetScale.y, percent);
        scale.z = 1.0f;
        transform.localScale = scale;
    }

    public void StartEffect()
    {
        if (_effect)
            _effect.Play();
    }
}
