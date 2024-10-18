using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QTEController : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private FadingGuideUI _fading;

    [SerializeField]
    private float _requiredDarkLight = 10.0f;

    [SerializeField]
    private float _totalTime = 2.0f;
    private float _currentRatio = 0.0f;

    [SerializeField]
    private Image _leftSide;
    [SerializeField]
    private Image _rightSide;
    [SerializeField]
    private Image _centerMask;
    [SerializeField]
    private Image _centerGlow;

    private float _scaleRatio = 0.5f;

    private bool _isActivated = false;

    [SerializeField]
    private SFXPlayer _sfx;

    [HideInInspector]
    [SerializeField]
    private ParticleSystem _darkLightEffect;
    [HideInInspector]
    [SerializeField]
    private ParticleSystemForceField _force;
    [HideInInspector]
    [SerializeField]
    private ParticleSystem _doneEffect;

    [HideInInspector]
    [SerializeField]
    private Animator _anim;

    public UnityEvent OnFull;

    private PlayerMoveController _player;

    [SerializeField]
    private bool _isReusable = false;
    private bool _isDone = false;
    [SerializeField]
    private Collider2D _relativeCol;

    private PlayerInventory _inventory;
    public float maxRatio
    {
        get
        {
            if (!_inventory)
                _inventory = FindObjectOfType<PlayerInventory>();

            if (!_inventory) return 0.0f;

            float ratio = _inventory.darkLight / _requiredDarkLight;
            return (ratio < 1.0f ? ratio : 1.0f);
        }
    }

    [SerializeField]
    private GlowUpAndDown _glowBlink;
    [SerializeField]
    private GlowableObject _glow;

    [SerializeField]
    private GameObject _noDarkLightImage;

    void Start()
    {
        _force.endRange = 1000.0f;
        _darkLightEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    private AudioSource _loop;
    void Update()
    {
        if (!_isActivated) return;

        if (KeyManager.instance.IsOnceKeyDown("scene_skip", true))
        {
            Deactivate();
            return;
        }

        if (KeyManager.instance.IsStayKeyDown("qte_active", true))
        {
            if (_currentRatio <= 0.0f)
            {
                StopLoopSFX();

                if (_sfx)
                {
                    _loop = _sfx.PlayLoopSFX("charge");
                }
            }

            _currentRatio += IsterTimeManager.originDeltaTime / _totalTime;

            if (_currentRatio >= maxRatio)
                _force.drag = 0.8f;
            else
                _force.drag = 0.03f;
        }
        else
        {
            _currentRatio -= IsterTimeManager.originDeltaTime / _totalTime * 3.0f;
            _force.drag = 0.8f;

            StopLoopSFX();
        }
        _currentRatio = Mathf.Clamp(_currentRatio, 0.0f, maxRatio);

        if (_currentRatio >= 1.0f)
            FullCharged();

        if (_currentRatio < _scaleRatio)
        {
            if (_currentRatio > float.Epsilon)
                ApplyScale(_currentRatio * (1.0f / _scaleRatio));
        }
        else
            ApplyRatio((_currentRatio - _scaleRatio) * (1.0f / _scaleRatio));
    }

    void StopLoopSFX()
    {
        if (_loop)
        {
            SoundSystem.instance.StopLoopSFX(_loop);
            _loop = null;
        }
    }

    void FullCharged()
    {
        StopLoopSFX();

        _isActivated = false;

        _anim.SetTrigger("full_charged");

        if (_sfx)
            _sfx.PlaySFX("done");

        _doneEffect.Play();

        _isDone = true;
        _relativeCol.enabled = _isReusable;
    }

    public void FullChargedEnd()
    {
        if (OnFull != null)
            OnFull.Invoke();

        StopLoopSFX();

        Deactivate();
    }

    public void Activate()
    {
        if (_isDone && !_isReusable) return;

        if (!_inventory)
            _inventory = FindObjectOfType<PlayerInventory>();

        _isDone = false;
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _player.PlayerMoveFreeze(true);

        _darkLightEffect.Play();

        _fading.StartFading(1.0f);

        FindObjectOfType<CameraBlurController>().StartBlur(50.0f);

        _isActivated = true;

        _currentRatio = 0.0f;
        ApplyRatio(_currentRatio);

        if (_sfx)
            _sfx.PlaySFX("active");

        if (_noDarkLightImage)
            _noDarkLightImage.SetActive((_inventory.darkLight == 0));
    }

    public void Deactivate()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _player.PlayerMoveFreeze(false);

        _darkLightEffect.Stop();

        _fading.StartFading(0.0f);

        FindObjectOfType<CameraBlurController>().StartBlur(1.0f);

        _isActivated = false;

        if (_sfx)
            _sfx.PlaySFX("deactive");

        StopLoopSFX();

        if (_glowBlink)
            _glowBlink.enabled = false;
        if (_glow)
            _glow.SetIntensity(0.0f);
    }

    void ApplyScale(float ratio)
    {
        Vector3 scale = _centerMask.transform.localScale;
        scale.x = ratio;
        scale.y = ratio;
        scale.z = 1.0f;
        _centerMask.transform.localScale = scale;

        scale.x = 1.0f / scale.x;
        scale.y = 1.0f / scale.y;
        scale.z = 1.0f;
        _centerGlow.transform.localScale = scale;
    }

    void ApplyRatio(float ratio)
    {
        _leftSide.fillAmount = ratio;
        _rightSide.fillAmount = ratio;
    }
}
