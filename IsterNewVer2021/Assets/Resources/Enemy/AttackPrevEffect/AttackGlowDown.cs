using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackGlowDown : AttackPrevEffect
{
    // Start is called before the first frame update
    

    // Update is called once per frame
    protected override IEnumerator DamagerOn()
    {
        Color color = _image.color;
        color.a = Mathf.Lerp(0.6f, 0.0f, 0.0f);

        while (_currentCount < _delayCount)
        {
            _currentCount += IsterTimeManager.deltaTime;

            float ratio = _currentCount / _delayCount;

            color.a = Mathf.Lerp(0.6f, 0.0f, ratio);

            _image.color = color;

            yield return null;
        }

        _damager.SetActive(true);

        ParticleSystem effect = _damager.GetComponent<ParticleSystem>();
        if (effect)
        {
            effect.Play();
            _sfx.PlaySFX("attackSound");
        }

        StopCoroutine(_coroutine);

        this.gameObject.SetActive(false);
    }
}
