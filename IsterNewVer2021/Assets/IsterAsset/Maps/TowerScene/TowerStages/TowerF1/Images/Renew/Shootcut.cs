using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootcut : MonoBehaviour
{
    private float _fadetimer;

    private float _vanishingTimer;

    private bool _fadeEnd;

    [SerializeField]
    GameObject _collider;
    [SerializeField]
    GameObject _layer;

    private SpriteRenderer _image;
    private Collider2D _selfCollider;

    private float _currentTimer;

    [SerializeField]
    private SFXPlayer _sfx;

    [SerializeField]
    private GameObject _hitEffectPrefab;

    [SerializeField]
    ParticleSystem _particle;

    // Start is called before the first frame update
    public void EnableShortcut()
    {
        _fadeEnd = false ;
        _currentTimer = 0;
        StartCoroutine(FadeIn());

        _particle.gameObject.SetActive(true);
        _particle.Play();
    }
    void Start()
    {
        _fadetimer = 1.0f;
        _vanishingTimer = 1.0f;

        _image = GetComponent<SpriteRenderer>();
        _selfCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_fadeEnd)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
            {
                if (_sfx)
                    _sfx.PlaySFX("hit");

                if (_hitEffectPrefab)
                {
                    GameObject effect = Instantiate(_hitEffectPrefab);
                    effect.transform.position = transform.position;
                }

                CameraShakeController.instance.CameraShake(10.0f);

                StartCoroutine(Vanishing());
            }
        }
    }

    IEnumerator FadeIn()
    {
        Color color = _image.color;

        color.a = Mathf.Lerp(0.0f, 1.0f, 0.0f);

        while (_currentTimer < _fadetimer)
        {
            _currentTimer += IsterTimeManager.deltaTime;

            float ratio = _currentTimer / _fadetimer;

            color.a = Mathf.Lerp(0.0f, 1.0f, ratio);

            _image.color = color;

            yield return null;
        }
  
        _fadeEnd = true;
        _currentTimer = 0.0f;
    }
    IEnumerator Vanishing()
    {
        _particle.loop = false;
        
        Color color = _image.color;

        color.a = Mathf.Lerp(1.0f, 0.0f, 0.0f);

        while (_currentTimer < _vanishingTimer)
        {
            _currentTimer += IsterTimeManager.deltaTime;

            float ratio = _currentTimer / _vanishingTimer;

            color.a = Mathf.Lerp(1.0f, 0.0f, ratio);
            _image.color = color;
            _layer.GetComponent<SpriteRenderer>().color = color;

            yield return null;
        }

        _layer.gameObject.SetActive(false);
        _collider.gameObject.SetActive(false);
        _particle.gameObject.SetActive(false);
        _selfCollider.enabled = false;
    }
}
