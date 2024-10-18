using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarController : MonoBehaviour
{
    [SerializeField]
    private Damagable _relativeBoss;

    [SerializeField]
    private Image _front;
    [SerializeField]
    private Image _middle;

    private bool _isBattleStart = false;
    public bool isBattleStart { get { return _isBattleStart; } }

    private Graphic[] _graphics;

    [SerializeField]
    private float _fillAmountMultiplier = 1.0f;

    void Start()
    {
        _graphics = GetComponentsInChildren<Graphic>();
        ApplyAlpha(0.0f);
        _front.fillAmount = 0.0f;
    }

    void Update()
    {
        _middle.fillAmount = Mathf.Lerp(_middle.fillAmount, _front.fillAmount, 0.1f);

        if (_isBattleStart)
        {
            float ratio = _relativeBoss.currentHP / _relativeBoss.totalHP;
            _front.fillAmount = ratio * _fillAmountMultiplier;
        }


        if (_middle.fillAmount < _front.fillAmount)
            _middle.fillAmount = _front.fillAmount;
    }

    public void StartFading(float from, float to)
    {
        StartCoroutine(Fading(from, to));
    }

    public void StartBattle()
    {
        StartFading(0.0f, 1.0f);
        StartCoroutine(FillFront());
    }

    public void EndBattle()
    {
        StartCoroutine(Fading(1.0f, 0.0f));
    }

    IEnumerator FillFront()
    {
        float currentTime = 0.0f;
        float fillTime = 1.0f;
        while (currentTime < fillTime)
        {
            float ratio = currentTime / fillTime;
            _front.fillAmount = _middle.fillAmount = ratio;

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _front.fillAmount = _middle.fillAmount = 1.0f;

        _isBattleStart = true;
    }

    IEnumerator Fading(float fromFade, float toFade)
    {
        float currentTime = 0.0f;
        float totalTime = 0.7f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;
            float alpha = Mathf.Lerp(fromFade, toFade, ratio);
            ApplyAlpha(alpha);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        ApplyAlpha(toFade);

        if (toFade < float.Epsilon)
            _isBattleStart = false;
    }

    void ApplyAlpha(float alpha)
    {
        foreach (var g in _graphics)
        {
            Color color = g.color;
            color.a = alpha;
            g.color = color;
        }
    }
}
