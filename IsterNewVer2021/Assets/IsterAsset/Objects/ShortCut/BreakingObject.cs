using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BreakingObject : MonoBehaviour
{
    [SerializeField]
    private float _vanishingTimer = 0.3f;
    private float _currentTimer;

    [SerializeField]
    private SpriteRenderer _crakedWall;

    [SerializeField]
    private SpriteRenderer[] _cracks;

    private int hitCount { get { return _cracks.Length; } }
    private int _currentHitCount;

    private bool _allCrackOn;

    private Collider2D _selfCollider;

    [SerializeField]
    private SFXPlayer _sfx;

    private SpriteRenderer _image;

    [SerializeField]
    private GameObject _hitEffectPrefab;
    [HideInInspector]
    [SerializeField]
    private Transform _effectPos;

    bool _breaking;
    bool _onActive;

    public UnityEvent OnBreak;

    //[SerializeField]
    //ParticleSystem _particle;

    // Start is called before the first frame update
    public void EnableShortcut()
    {
        _onActive = true;

        if (_sfx)
            _sfx.PlaySFX("hit");

        if (_hitEffectPrefab)
        {
            GameObject effect = Instantiate(_hitEffectPrefab);
            effect.transform.position = transform.position;
        }

        CameraShakeController.instance.CameraShake(10.0f);
    }
    void Start()
    {
        _onActive = true;

        _allCrackOn = false;
        _breaking = false;
        _currentTimer = 0.0f;
        _currentHitCount = 0;
        _image = GetComponent<SpriteRenderer>();

        _selfCollider = GetComponent<Collider2D>();

        foreach (var c in _cracks)
            c.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            if (_onActive)
            {
                if (_sfx)
                    _sfx.PlaySFX("hit");

                if (_hitEffectPrefab)
                {
                    GameObject effect = Instantiate(_hitEffectPrefab);
                    effect.transform.position = _effectPos.position;
                }

                CameraShakeController.instance.CameraShake(10.0f);

                _currentHitCount++;

                if (_currentHitCount <= _cracks.Length)
                    _cracks[_currentHitCount - 1].gameObject.SetActive(true);

                if (!_allCrackOn)
                {
                    if (_currentHitCount >= _cracks.Length)
                        _allCrackOn = true;
                }
                else
                {
                    if (!_breaking)
                    {
                        if (_currentHitCount >= hitCount)
                            StartCoroutine(Vanishing());
                    }
                }
            }
        }
        //else
        //{
        //    EnableShortcut();
        //    _currentHitCount++;
        //}

        //if (_onActive)
        //{
        //    if (!_allCrackOn)
        //    {
        //        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        //        {
        //            if (_sfx)
        //                _sfx.PlaySFX("hit");

        //            if (_hitEffectPrefab)
        //            {
        //                GameObject effect = Instantiate(_hitEffectPrefab);
        //                effect.transform.position = transform.position;
        //            }

        //            CameraShakeController.instance.CameraShake(10.0f);

        //            _currentHitCount++;
        //            _cracks[_currentHitCount - 1].SetActive(true);

        //            if (_currentHitCount >= _cracks.Length)
        //                _allCrackOn = true;
        //        }
        //    }
        //    else
        //    {
        //        if (!_breaking)
        //        {
        //            if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        //            {
        //                if (_sfx)
        //                    _sfx.PlaySFX("hit");

        //                if (_hitEffectPrefab)
        //                {
        //                    GameObject effect = Instantiate(_hitEffectPrefab);
        //                    effect.transform.position = transform.position;
        //                }

        //                CameraShakeController.instance.CameraShake(10.0f);
        //                _currentHitCount++;

        //                if (_currentHitCount >= hitCount)
        //                {
        //                    StartCoroutine(Vanishing());
        //                }
        //            }
        //        }
        //    }
        //}
        //else 
        //{
        //    if(collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        //    {
        //        EnableShortcut();
        //        _currentHitCount++;
        //    }
        //}
    }

    IEnumerator Vanishing()
    {
        if (_sfx)
            _sfx.PlaySFX("break");

        if (_hitEffectPrefab)
        {
            GameObject effect = Instantiate(_hitEffectPrefab);
            effect.transform.position = _effectPos.position;
            effect.transform.localScale = new Vector3(3.0f, 3.0f, 3.0f);
        }

        _breaking = true;

        while (_currentTimer < _vanishingTimer)
        {
            _currentTimer += IsterTimeManager.deltaTime;

            float ratio = _currentTimer / _vanishingTimer;

            ApplyAlpha(1.0f - ratio);

            //_crack1.GetComponent<SpriteRenderer>().color = color;
            //_crack2.GetComponent<SpriteRenderer>().color = color;
            //_crack2.GetComponent<SpriteRenderer>().color = color;

            yield return null;
        }
        ApplyAlpha(0.0f);

        _selfCollider.enabled = false;
        this.gameObject.SetActive(false);

        if (OnBreak != null)
            OnBreak.Invoke();
    }

    void ApplyAlpha(float alpha)
    {
        Color color;

        color = _image.color;
        color.a = alpha;
        _image.color = color;

        color = _crakedWall.color;
        color.a = alpha;
        _crakedWall.color = color;

        foreach (var c in _cracks)
        {
            color = c.color;
            color.a = alpha;
            c.color = color;
        }
    }
}
