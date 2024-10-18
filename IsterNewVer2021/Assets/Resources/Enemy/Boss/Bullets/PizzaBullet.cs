using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaBullet : MonoBehaviour
{
    [SerializeField]
    GameObject _damager;

    public float _delayCount;
    float _currentCount;

    private SpriteRenderer _image;

    [SerializeField]
    private SFXPlayer _sfx;
    // Start is called before the first frame update
    private void OnEnable()
    {
        _currentCount = 0;
        _image= GetComponentInChildren<SpriteRenderer>();
        StartCoroutine(DamagerOn());
    }
    IEnumerator DamagerOn()
    {
        Color color = _image.color;
        color.a = Mathf.Lerp(0.3f, 0.0f, 0.0f);

        while (_currentCount < _delayCount)
        {   
            _currentCount += IsterTimeManager.deltaTime;

            float ratio = _currentCount / _delayCount;

            color.a = Mathf.Lerp(0.3f, 0.0f, ratio);
             
            _image.color = color;

            yield return null;
        }

        _damager.SetActive(true);

        ParticleSystem effect = _damager.GetComponent<ParticleSystem>();
        if (effect)
        {
            effect.Play();
            _sfx.PlaySFX("pizzaAttack");
        }

        StopCoroutine(DamagerOn());

        this.gameObject.SetActive(false); 
    }
}
