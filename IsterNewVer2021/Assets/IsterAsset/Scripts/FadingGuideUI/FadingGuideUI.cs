using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Calcatz.ArrivalGUI;

public class FadingGuideUI : FadingMenuBase
{
    [SerializeField]
    private Graphic[] _graphics;
    public Graphic[] graphics { get { return _graphics; } }

    /// <summary>
    /// Called on Awake
    /// </summary>
    private void Awake()
    {
        if (_graphics != null)
        {
            if (_graphics.Length > 0)
            {
                foreach (var g in _graphics)
                {
                    FadingUIAlphaHelper helper = g.GetComponent<FadingUIAlphaHelper>();
                    if (helper)
                        ApplyAlpha(g, helper.alphaMultiplier);
                }

                List<Graphic> graphics = new List<Graphic>();
                graphics.AddRange(_graphics);
                InitializeGraphicAlphas(graphics);
            }
            else
            {
                List<Graphic> graphics = new List<Graphic>();
                _graphics = GetComponentsInChildren<Graphic>();
                graphics.AddRange(_graphics);
                InitializeGraphicAlphas(graphics);
            }
        }
        else
        {
            List<Graphic> graphics = new List<Graphic>();
            _graphics = GetComponentsInChildren<Graphic>();
            graphics.AddRange(_graphics);
            InitializeGraphicAlphas(graphics);
        }
        //ApplyAlpha(_startAlpha);
        //if (!show)
        //{
        //    if (dimBackground != null) dimBackground.gameObject.SetActive(false);
        //    if (alertPanel != null) alertPanel.gameObject.SetActive(false);
        //    if (navigationButtons != null) navigationButtons.gameObject.SetActive(false);
        //}
    }

    [SerializeField]
    private float _startAlpha = 0.0f;
    [SerializeField]
    private bool _isRotationFix = false;

    void Start()
    {
        //if (_graphics == null)
        //    _graphics = GetComponentsInChildren<Graphic>();
        //else if (_graphics.Length == 0)
        //    _graphics = GetComponentsInChildren<Graphic>();

        //_currentFadeTime = _totalFadeTime;

        //ApplyAlpha(_startAlpha);
        //_currentAlpha = _startAlpha;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        ApplyAlpha(0.0f);
    }

    protected override void Update()
    {
        base.Update();

        if (_isRotationFix)
            transform.rotation = Quaternion.identity;
    }

    //[SerializeField]
    //private float _totalFadeTime = 0.15f;
    public float totalFadeTime { get { return fadeDuration; } set { fadeDuration = value; } }
    //private int _coroutineCount = 0;
    //private float _currentFadeTime = 0.0f;
    //private float _currentAlpha = 0.0f;
    //public float currentAlpha { get { return _currentAlpha; } set { _currentAlpha = value; } }
    public void StartFading(float toAlpha)
    {
        if (toAlpha > 0.0f)
            show = true;
        else
            show = false;

        //_graphics = GetComponentsInChildren<Graphic>();

        //StartCoroutine(Fading(toAlpha, false));
    }
    public void StartFading(float toAlpha, float duration)
    {
        fadeDuration = duration;
        if (toAlpha > 0.0f)
            show = true;
        else
            show = false;

        //_graphics = GetComponentsInChildren<Graphic>();

        //StartCoroutine(Fading(toAlpha, false));
    }

    public void StartFading(float toAlpha, bool isIgnorePrev = false)
    {
        if (isIgnorePrev)
        {
            if (showCoroutine != null)
                StopCoroutine(showCoroutine);
        }

        if (toAlpha > 0.0f)
            show = true;
        else
            show = false;

        //_graphics = GetComponentsInChildren<Graphic>();

        //StartCoroutine(Fading(toAlpha, isIgnorePrev));
    }

    //IEnumerator Fading(float toAlpha, bool isIgnorePrev = false)
    //{
    //    _coroutineCount++;
    //    if (isIgnorePrev)
    //        _currentFadeTime = 0.0f;
    //    else
    //        _currentFadeTime = _totalFadeTime - _currentFadeTime;
    //    float startAlpha = _currentAlpha;
    //    while (_currentFadeTime < _totalFadeTime)
    //    {
    //        float ratio = _currentFadeTime / _totalFadeTime;

    //        _currentAlpha = Mathf.Lerp(startAlpha, toAlpha, ratio);
    //        ApplyAlpha(_currentAlpha);

    //        yield return null;

    //        _currentFadeTime += IsterTimeManager.deltaTime;

    //        if (_coroutineCount > 1)
    //        {
    //            _coroutineCount--;
    //            yield break;
    //        }
    //    }
    //    _currentFadeTime = _totalFadeTime;

    //    _currentAlpha = Mathf.Lerp(startAlpha, toAlpha, 1.0f);
    //    ApplyAlpha(_currentAlpha);

    //    _coroutineCount--;
    //}

    void ApplyAlpha(Graphic g, float alpha)
    {
        Color color = g.color;
        color.a = alpha;
        g.color = color;
    }

    public void ApplyAlpha(float alpha)
    {
        if (_graphics == null) return;

        //_currentAlpha = alpha;
        foreach (var g in _graphics)
        {
            float toAlpha = alpha;
            if (typeof(Image).IsInstanceOfType(g))
            {
                FadingUIAlphaHelper helper = g.GetComponent<FadingUIAlphaHelper>();
                if (helper)
                    toAlpha *= helper.alphaMultiplier;
                else
                    toAlpha *= 0.3f;
            }

            Color color = g.color;
            color.a = toAlpha;
            g.color = color;
        }
    }


    /// <summary>
    /// Animates panel fade-in.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator ShowCoroutine()
    {
        OnBeforeShow();

        List<Color> colorList = new List<Color>();
        foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            colorList.Add(pair.Key.color);

        for (float t = 0; t < fadeDuration; t += IsterTimeManager.originDeltaTime)
        {
            int index = 0;
            foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            {
                Color targetColor = colorList[index];
                targetColor.a = pair.Value;
                pair.Key.color = Color.Lerp(colorList[index], targetColor, Mathf.Clamp01(t / fadeDuration));
                index++;
            }
            yield return null;
        }

        {
            int index = 0;
            foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            {
                Color targetColor = colorList[index];
                targetColor.a = pair.Value;
                pair.Key.color = Color.Lerp(colorList[index], targetColor, 1.0f);
                index++;
            }
        }
    }

    /// <summary>
    /// Animates panel fade-out.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator CloseCoroutine()
    {
        List<Color> colorList = new List<Color>();
        foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            colorList.Add(pair.Key.color);

        for (float t = 0; t < fadeDuration; t += IsterTimeManager.originDeltaTime)
        {
            int index = 0;
            foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            {
                Color targetColor = colorList[index];
                targetColor.a = 0f;
                pair.Key.color = Color.Lerp(colorList[index], targetColor, Mathf.Clamp01(t / fadeDuration));
                index++;
            }
            yield return null;
        }

        {
            int index = 0;
            foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            {
                Color targetColor = colorList[index];
                targetColor.a = 0f;
                pair.Key.color = Color.Lerp(colorList[index], targetColor, 1.0f);
                index++;
            }
        }
        OnAfterClose();
    }
}
