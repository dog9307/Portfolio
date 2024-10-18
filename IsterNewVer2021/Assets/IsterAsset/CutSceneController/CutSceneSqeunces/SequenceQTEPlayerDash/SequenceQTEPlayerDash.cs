using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class SequenceQTEPlayerDash : CutSceneSqeunceBase
{
    [Header("QTE Á¶Àý")]
    [HideInInspector]
    [SerializeField]
    private Slider _slider;

    [HideInInspector]
    [SerializeField]
    private FadingGuideUI _fading;
    private Dictionary<Graphic, Color> _originUIColorDic = new Dictionary<Graphic, Color>();

    private bool _isSuccess = false;

    public UnityEvent OnSuccess;
    public UnityEvent OnFail;

    [HideInInspector]
    [SerializeField]
    private ParticleSystem _successEffect;

    [HideInInspector]
    [SerializeField]
    private RectTransform _sliderRoot;
    private Vector2 _startUIPos;
    [HideInInspector]
    [SerializeField]
    private Graphic[] _effectableGraphics;
    [SerializeField]
    private Color _failOutOfRangeColor;
    [SerializeField]
    private Color _failTimeOverColor;

    [HideInInspector]
    [SerializeField]
    private RectTransform _targetAreaRc;

    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float _minTargetValue = 0.3f;
    [Range(0.0f, 1.0f)]
    [SerializeField]
    private float _maxTargetValue = 0.7f;

    [SerializeField]
    private string _targetKey = "dash";
    [HideInInspector]
    [SerializeField]
    private TextMeshProUGUI _keyText;

    void Start()
    {
        Vector2 size = _targetAreaRc.sizeDelta;
        Vector2 pos = _targetAreaRc.anchoredPosition;

        pos.x = ((_minTargetValue + _maxTargetValue) / 2.0f) * size.x - (size.x / 2.0f);
        _targetAreaRc.anchoredPosition = pos;

        size.x = (_maxTargetValue - _minTargetValue) * size.x;
        _targetAreaRc.sizeDelta = size;

        KeyCode code = KeyManager.instance.GetKeyCode(_targetKey);
        if (code == KeyCode.None) return;

        _keyText.text = code.ToString().ToUpper();
    }

    void Update()
    {
        if (!_isDuringSequence) return;
        if (_isSuccess) return;

        if (KeyManager.instance.IsOnceKeyDown(_targetKey, true))
        {
            if (_minTargetValue <= _slider.value && _slider.value <= _maxTargetValue)
                _isSuccess = true;
            else
            {
                ShakeUI();
                UIColorBlink(_failOutOfRangeColor);
            }
        }
    }

    private Coroutine _shakeCoroutine;
    public void ShakeUI()
    {
        if (_shakeCoroutine != null)
            StopCoroutine(_shakeCoroutine);

        _shakeCoroutine = StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float currentTime = 0.0f;
        float totalTime = 0.2f;
        float startRadius = 20.0f;
        float angle = 0.0f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            float radius = Mathf.Lerp(startRadius, 0.0f, ratio);
            angle = (angle + Random.Range(120.0f, 150.0f));
            if (angle >= 360.0f)
                angle -= 360.0f;

            Vector2 newPos = _startUIPos + new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;
            _sliderRoot.anchoredPosition = newPos;

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _sliderRoot.anchoredPosition = _startUIPos;
    }

    private Coroutine _colorBlinkCoroutine;
    public void UIColorBlink(Color targetColor)
    {
        if (_colorBlinkCoroutine != null)
            StopCoroutine(_colorBlinkCoroutine);

        _colorBlinkCoroutine = StartCoroutine(ColorChange(targetColor));
    }

    IEnumerator ColorChange(Color toColor)
    {
        float currentTime = 0.0f;
        float totalTime = 0.1f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            foreach (var g in _originUIColorDic)
            {
                Color color = Color.Lerp(g.Value, toColor, ratio);
                color.a = g.Key.color.a;
                g.Key.color = color;
            }

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }

        currentTime = 0.0f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            foreach (var g in _originUIColorDic)
            {
                Color color = Color.Lerp(toColor, g.Value, ratio);
                color.a = g.Key.color.a;
                g.Key.color = color;
            }

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        foreach (var g in _originUIColorDic)
        {
            Color color = g.Value;
            color.a = g.Key.color.a;
            g.Key.color = color;
        }

        _colorBlinkCoroutine = null;
    }

    protected override IEnumerator DuringSequence()
    {
        _isSuccess = false;

        _startUIPos = _sliderRoot.anchoredPosition;

        _fading.StartFading(1.0f);

        if (_originUIColorDic == null)
            _originUIColorDic = new Dictionary<Graphic, Color>();

        _originUIColorDic.Clear();
        foreach (var g in _effectableGraphics)
            _originUIColorDic.Add(g, g.color);

        float currentTime = 0.0f;
        while (currentTime < _sequenceTime && !_isSuccess)
        {
            float ratio = currentTime / _sequenceTime;

            _slider.value = ratio;

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _fading.StartFading(0.0f);

        _isDuringSequence = false;

        if (_isSuccess)
        {
            PlayerDash();

            if (_successEffect)
                _successEffect.Play();

            if (OnSuccess != null)
                OnSuccess.Invoke();
        }
        else
        {
            _slider.value = 1.0f;

            ShakeUI();
            UIColorBlink(_failTimeOverColor);

            if (OnFail != null)
                OnFail.Invoke();
        }
    }

    public void PlayerDash()
    {
        PlayerSkillUsage playerSkill = FindObjectOfType<PlayerSkillUsage>();
        playerSkill.UseDash();
    }
}
