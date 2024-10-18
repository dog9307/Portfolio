using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shortcut : MonoBehaviour
{

    private float _currentTimer;
    private float _vanishingTimer;
    [SerializeField]
    GameObject _collider;
    [SerializeField]
    GameObject _layer;

    [SerializeField]
    GameObject _crack1, _crack2;

    private bool _allCrackOn;

    private Collider2D _selfCollider;

    [SerializeField]
    private SFXPlayer _sfx;

    private SpriteRenderer _image;

    [SerializeField]
    private GameObject _hitEffectPrefab;

    bool _breaking;
    bool _onActive;

    //[SerializeField]
    //ParticleSystem _particle;

    // Start is called before the first frame update
    public void EnableShortcut()
    {
        _onActive = true;
        _crack1.gameObject.SetActive(true);

        if (_sfx)
            _sfx.PlaySFX("hit");

        if (_hitEffectPrefab)
        {
            GameObject effect = Instantiate(_hitEffectPrefab);
            effect.transform.position = transform.position;
        }

        CameraShakeController.instance.CameraShake(10.0f);
        //   _particle.gameObject.SetActive(true);
        //   _particle.Play();
    }
    void Start()
    {
        _allCrackOn = false;
        _breaking = false;
         _vanishingTimer = 1.0f;
        _currentTimer = 0;

        _image = GetComponent<SpriteRenderer>();

        _selfCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_onActive) {
            if (!_allCrackOn)
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

                    _crack2.gameObject.SetActive(true);
                    _allCrackOn = true;
                }
            }
            else
            {
                if (!_breaking)
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
        } 
    }

    IEnumerator Vanishing()
    {
        _breaking = true;
        Color color = _image.color;

        color.a = Mathf.Lerp(1.0f, 0.0f, 0.0f);

        while (_currentTimer < _vanishingTimer)
        {
            _currentTimer += IsterTimeManager.deltaTime;

            float ratio = _currentTimer / _vanishingTimer;

            color.a = Mathf.Lerp(1.0f, 0.0f, ratio);
            _image.color = color;
            _crack1.GetComponent<SpriteRenderer>().color = color;
            _crack2.GetComponent<SpriteRenderer>().color = color;
            _layer.GetComponent<SpriteRenderer>().color = color;

            yield return null;
        }

        _layer.gameObject.SetActive(false);
        _collider.gameObject.SetActive(false);
        _selfCollider.enabled = false;
        this.gameObject.SetActive(false);
    }
}
