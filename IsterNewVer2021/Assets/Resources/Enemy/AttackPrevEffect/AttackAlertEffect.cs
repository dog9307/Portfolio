using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAlertEffect : AttackPrevEffect
{
    [SerializeField]
    GameObject _bullet;
    [SerializeField]
    ParticleSystem _alertEffect;

    public float _posX, _posY;

    public float _size;

    public float _alertCount;
    float _currentAlertCount;

    Vector3 _currentScale; 
    // Start is called before the first frame update

    protected override void OnEnable()
    {
        base.OnEnable();
        _currentAlertCount = 0;
        _currentScale = transform.localScale;
    }
    protected override IEnumerator DamagerOn()
    {
        float attackScale = transform.localScale.x;
        attackScale = Mathf.Lerp(0.0f, _currentScale.x, 0.0f);

        while (_currentCount < _delayCount)
        {
            _currentCount += IsterTimeManager.deltaTime;

            float ratio = _currentCount / _delayCount;

            attackScale = Mathf.Lerp(0.0f, _currentScale.x, ratio);

            transform.localScale = new Vector3(attackScale, attackScale, attackScale);

            yield return null;
        }

        _coroutine = StartCoroutine(AlertAttack());
    }

    IEnumerator AlertAttack()
    {
        _alertEffect.Play();

        while (_alertCount < _currentAlertCount)
        {
            _currentAlertCount += IsterTimeManager.deltaTime;

            yield return null;
        }

        //_damager.SetActive(true);
        //
        //ParticleSystem effect = _damager.GetComponent<ParticleSystem>();
        //if (effect)
        //{
        //    effect.Play();
        //}

        GameObject newBullet = CreateObject();
        newBullet.transform.localScale = (newBullet.transform.localScale * _size);
        newBullet.transform.position = new Vector2(transform.position.x + _posX, transform.position.y + _posY);

        StopCoroutine(_coroutine);

        this.gameObject.SetActive(false);
    }
    GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(_bullet);
        newBullet.transform.position = transform.position;
        return newBullet;
    }
}
