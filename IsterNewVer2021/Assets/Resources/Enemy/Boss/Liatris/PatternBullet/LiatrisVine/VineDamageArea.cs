using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineDamageArea : MonoBehaviour
{
    [SerializeField]
    Vine _vine;
    public float _delayCount;
    float _currentCount;

    private SpriteRenderer _image;

    public float _phase2Multiple;

    Coroutine _coroutine;
    
    // Start is called before the first frame update
    private void OnEnable()
    {
        _currentCount = 0;
        _image = GetComponentInChildren<SpriteRenderer>();
        _coroutine = StartCoroutine(DamagerOn());
    }
    private void Update()
    {
        if (_vine._phaseChager.isPhaseChanging)
        {
            _currentCount = 0;
            if(_coroutine != null) StopCoroutine(_coroutine);

            this.gameObject.SetActive(false);
        }
    }
    IEnumerator DamagerOn()
    {
        Color color = _image.color;
        color.a = Mathf.Lerp(0.0f, 0.6f, 0.0f);

        while (_currentCount < _delayCount)
        {
            if (_vine._controller._isPhaseChage)
            {
                _currentCount += (IsterTimeManager.bossDeltaTime * _phase2Multiple);
            }
            else
                _currentCount += IsterTimeManager.bossDeltaTime;

            float ratio = _currentCount / _delayCount;

            color.a = Mathf.Lerp(0.0f, 0.6f, ratio);

            _image.color = color;

            yield return null;
        }

        _vine.AttackStart();

        StopCoroutine(_coroutine);

        this.gameObject.SetActive(false);
    }
}
