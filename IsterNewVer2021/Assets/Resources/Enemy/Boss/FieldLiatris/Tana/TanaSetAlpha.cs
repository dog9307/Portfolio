using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanaSetAlpha : MonoBehaviour
{
    public float _hidingTime;

    public float _dieTimer;
    public float _deadTimer;
    [SerializeField]
    SpriteRenderer _renderer;

    float _currentTime;
    float _currentDieTimer;
    float _currentDeadTimer;

    Coroutine _coroutine;
    // Start is called before the first frame update
    void Start()
    {
        _currentTime = 0;
        _currentDieTimer = 0;
    }

    public void HideOn()
    { 
        _currentTime=0;
        _coroutine = StartCoroutine(TanaHideOn());
    }
    public void HideOff()
    {
        _currentTime = 0;
        _coroutine = StartCoroutine(TanaHideOff());
    }
    public void Die()
    {
        _currentDieTimer = 0;
           _coroutine = StartCoroutine(TanaDie());
    }
    public void Dead()
    {
        _currentDeadTimer = 0;
           _coroutine = StartCoroutine(TanaDead());
    }
    public IEnumerator TanaHideOn()
    {
        Color color = _renderer.color;
        color.a = Mathf.Lerp(1.0f, 0.0f, 0.0f);

        while (_currentTime < _hidingTime)
        {
            _currentTime += IsterTimeManager.bossDeltaTime;

            float ratio = _currentTime / _hidingTime;

            color.a = Mathf.Lerp(1.0f, 0.0f, ratio);

            _renderer.color = color;

            yield return null;
        }

        _currentTime = 0;
        StopCoroutine(TanaHideOn());
    }

    public IEnumerator TanaHideOff()
    {
        Color color = _renderer.color;
        color.a = Mathf.Lerp(0.0f, 1.0f, 0.0f);

        while (_currentTime < _hidingTime)
        {
            _currentTime += IsterTimeManager.bossDeltaTime;

            float ratio = _currentTime / _hidingTime;

            color.a = Mathf.Lerp(0.0f, 1.0f, ratio);

            _renderer.color = color;

            yield return null;
        }

        _currentTime = 0;
        StopCoroutine(TanaHideOff());
    }
    public IEnumerator TanaDie()
    {
        Color color = _renderer.color; 
        
        color.r = Mathf.Lerp(1.0f, 0.15f, 0.0f);
        color.g = Mathf.Lerp(1.0f, 0.15f, 0.0f);
        color.b = Mathf.Lerp(1.0f, 0.15f, 0.0f);

        while (_currentDieTimer < _dieTimer)
        {
            _currentDieTimer += IsterTimeManager.bossDeltaTime;

            float ratio = _currentDieTimer / _dieTimer;

            color.r = Mathf.Lerp(1.0f, 0.15f, ratio);
            color.g = Mathf.Lerp(1.0f, 0.15f, ratio);
            color.b = Mathf.Lerp(1.0f, 0.15f, ratio);

            _renderer.color = color;

            yield return null;
        }

        _currentDieTimer = 0;
        StopCoroutine(TanaHideOn());
    }
    public IEnumerator TanaDead()
    {
        Color color = _renderer.color;
        color.a = Mathf.Lerp(1.0f, 0.0f, 0.0f);

        while (_currentDeadTimer < _deadTimer)
        {
            _currentDeadTimer += IsterTimeManager.bossDeltaTime;

            float ratio = _currentDeadTimer / _deadTimer;

            color.a = Mathf.Lerp(1.0f, 0.0f, ratio);

            _renderer.color = color;

            yield return null;
        }

        _currentDieTimer = 0;
        StopCoroutine(TanaHideOn());
    }
}
