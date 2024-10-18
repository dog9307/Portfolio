using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateWallPattern : MonoBehaviour , IObjectCreator
{
    [SerializeField]
    BossMain _liatris;
    [SerializeField]
    BossController _controller;

    [SerializeField]
    BossDamagableCondition _phaseChanger;

    [SerializeField]
    Animator _ani;

    [SerializeField]
    GameObject _createWallBullet;


    [SerializeField]
    GameObject _bulletObject;
    public GameObject effectPrefab { get { return _bulletObject; } set { _bulletObject = value; } }

    [SerializeField]
    Transform _createPos;

    public float _delayCount;
    float _currentCount;

    private SpriteRenderer _image;

    [SerializeField]
    float _maxScale;
    float _currentBulletScale;

    [SerializeField]
    ParticleSystem _seek;
    [SerializeField]
    ParticleSystem _hide;
    [SerializeField]
    ParticleSystem _hit;
    [SerializeField]
    private SFXPlayer _sfx;

    Coroutine _currentCoroutine;

    bool _isActive;
    public bool isActive { get { return _isActive; } set { _isActive = value; } }
    //bool _isAwake;
    //public bool isAwake { get { return _isAwake; } set { _isAwake = value; } }
    //bool _isPatternStart;

    //public float _coolTime;
    //float _currentCoolTime;

    [SerializeField]
    float _hitCount;
    float _currentHitCount;


    public float _phase2Multiple;
    // Start is called before the first frame update
    private void OnEnable()
    {
        if (_seek)
            _seek.Play();
        _createWallBullet.SetActive(false);
        _currentCount = 0;
        //_currentCoolTime = 0;
        _currentBulletScale = 0.0f;
        _isActive = true;
        //_isAwake = false;
        //_isPatternStart = false;

        _currentCoroutine = StartCoroutine(BulletReady());
        if (_sfx)
            _sfx.PlaySFX("charging");
        _currentHitCount = 0;
    }
    private void OnDisable()
    {
        _isActive = false;
    }
    private void Update()
    {
        _createWallBullet.transform.position = _createPos.position;

        if (!_liatris.damagable.isDie)
        {
            if (_phaseChanger.isPhaseChanging)
            {
                if(this.gameObject.activeSelf) HideCreator();
            }
        }
        else
        {
            BossDie();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            _currentHitCount++;

            if (_currentHitCount > _hitCount - 0.0000001f)
            {
                HideCreator();
            }

            else
            {
                if (_sfx)
                    _sfx.PlaySFX("hit");

                if (_hit)
                    _hit.Play();

                _ani.SetTrigger("Hit");

                CameraShakeController.instance.CameraShake(4.0f);
            }
        }
    }
    IEnumerator BulletReady()
    {
        _createWallBullet.SetActive(true);

        float scale = 0.0f;
        scale = Mathf.Lerp(0.0f, _maxScale, 0.0f);

        while (_currentCount < _delayCount)
        {
            if (_controller._isPhaseChage)
                _currentCount += (IsterTimeManager.bossDeltaTime * _phase2Multiple);
            else _currentCount += IsterTimeManager.bossDeltaTime;

            float ratio = _currentCount / _delayCount;

            scale = Mathf.Lerp(0.0f, _maxScale, ratio);

            _currentBulletScale = scale;

            _createWallBullet.transform.localScale = new Vector3( _currentBulletScale, _currentBulletScale, _currentBulletScale);

            yield return null;
        }

        CreateObject();

        _currentCount = 0;

        HideCreator();
        // _currentCoroutine = StartCoroutine(CoolTime());

    }
  //  IEnumerator CoolTime()
  //  {
  //      while (_currentCoolTime < _coolTime)
  //      {
  //          yield return null;
  //
  //          _currentCoolTime += IsterTimeManager.bossDeltaTime;
  //      }
  //
  //      _currentCoolTime = 0;
  //      _currentCoroutine = StartCoroutine(BulletReady());
  //  }
    public GameObject CreateObject()
    {
        _createWallBullet.SetActive(false);

        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        newBullet.transform.position = _createPos.position;
        newBullet.transform.localScale = new Vector3(_maxScale, _maxScale, _maxScale);
        return newBullet;

    }

    void HideCreator()
    {
        if (_hide)
            _hide.Play();

        _controller.gameObject.GetComponent<WallCreatorSpawnPattern>()._spawnStart = false;
        this.gameObject.SetActive(false);

        StopCoroutine(_currentCoroutine);
    }
    public void BossDie()
    {
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCount = 0;

        this.gameObject.SetActive(false);

        _createWallBullet.SetActive(false);
    }
}
