using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Calcatz.ArrivalGUI;

public class FadingUI : FadingMenuBase
{
    [SerializeField]
    private bool _isAutoFade = false;

    [SerializeField]
    protected Graphic[] _graphics;
    public Graphic[] graphics { get { return _graphics; } }

    [SerializeField]
    protected CanvasGroup[] _groups;
    public CanvasGroup[] groups { get { return _groups; } }

    [SerializeField]
    private float _showDelay = 0.0f;

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
    }

    [SerializeField]
    protected float _startAlpha = 0.0f;
    [SerializeField]
    protected bool _isRotationFix = false;

    [SerializeField]
    private bool _isApplyStartAlpha = true;

    void Start()
    {
        if (_isAutoFade)
            show = true;

        if (_isApplyStartAlpha)
            ApplyAlpha(_startAlpha);
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

    public float totalFadeTime { get { return fadeDuration; } set { fadeDuration = value; } }
    public void StartFading(float toAlpha)
    {
        if (toAlpha > 0.0f)
            show = true;
        else
            show = false;
    }
    public void StartFading(float toAlpha, float duration)
    {
        fadeDuration = duration;
        if (toAlpha > 0.0f)
            show = true;
        else
            show = false;
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
    }

    void ApplyAlpha(Graphic g, float alpha)
    {
        Color color = g.color;
        color.a = alpha;
        g.color = color;
    }

    public void ApplyAlpha(float alpha)
    {
        if (_graphics != null)
        {

            foreach (var g in _graphics)
            {
                if (!g) continue;

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

        if (groups != null)
        {
            foreach (var g in groups)
                g.alpha = alpha;
        }
    }


    /// <summary>
    /// Animates panel fade-in.
    /// </summary>
    /// <returns></returns> 

    public UnityEvent onBeforeShow;
    public UnityEvent onAfterShow;
    public UnityEvent onBeforeClose;
    public UnityEvent onAfterClose;

    protected override void OnBeforeShow()
    {
        if (onBeforeShow != null)
            onBeforeShow.Invoke();
    }

    protected void OnAfterShow()
    {
        if (onAfterShow != null)
            onAfterShow.Invoke();
    }

    protected void OnBeforeClose()
    {
        if (onBeforeClose != null)
            onBeforeClose.Invoke();
    }

    protected override void OnAfterClose()
    {
        if (onAfterClose != null)
            onAfterClose.Invoke();
    }

    protected override IEnumerator ShowCoroutine()
    {
        OnBeforeShow();

        yield return new WaitForSeconds(_showDelay);

        List<Color> colorList = new List<Color>();
        foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
        {
            if (!pair.Key) continue;

            colorList.Add(pair.Key.color);
        }

        for (float t = 0; t < fadeDuration; t += TimeManager.originDeltaTime)
        {
            int index = 0;
            foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            {
                if (!pair.Key) continue;

                Color targetColor = colorList[index];
                targetColor.a = pair.Value;
                pair.Key.color = Color.Lerp(colorList[index], targetColor, Mathf.Clamp01(t / fadeDuration));
                index++;
            }

            if (groups != null)
            {
                foreach (var g in groups)
                    g.alpha = Mathf.Lerp(0.0f, 1.0f, Mathf.Clamp01(t / fadeDuration));
            }

            yield return null;
        }

        {
            int index = 0;
            foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            {
                if (!pair.Key) continue;

                Color targetColor = colorList[index];
                targetColor.a = pair.Value;
                pair.Key.color = Color.Lerp(colorList[index], targetColor, 1.0f);
                index++;
            }
            if (groups != null)
            {
                foreach (var g in groups)
                    g.alpha = Mathf.Lerp(0.0f, 1.0f, 1.0f);
            }
        }

        OnAfterShow();
    }

    /// <summary>
    /// Animates panel fade-out.
    /// </summary>
    /// <returns></returns>
    protected override IEnumerator CloseCoroutine()
    {
        OnBeforeClose();

        List<Color> colorList = new List<Color>();
        foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
        {
            if (!pair.Key) continue;

            colorList.Add(pair.Key.color);
        }

        for (float t = 0; t < fadeDuration; t += TimeManager.originDeltaTime)
        {
            int index = 0;
            foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            {
                if (!pair.Key) continue;

                Color targetColor = colorList[index];
                targetColor.a = 0f;
                pair.Key.color = Color.Lerp(colorList[index], targetColor, Mathf.Clamp01(t / fadeDuration));
                index++;
            }

            if (groups != null)
            {
                foreach (var g in groups)
                    g.alpha = Mathf.Lerp(1.0f, 0.0f, Mathf.Clamp01(t / fadeDuration));
            }
            yield return null;
        }

        {
            int index = 0;
            foreach (KeyValuePair<Graphic, float> pair in initialAlphas)
            {
                if (!pair.Key) continue;

                Color targetColor = colorList[index];
                targetColor.a = 0f;
                pair.Key.color = Color.Lerp(colorList[index], targetColor, 1.0f);
                index++;
            }

            if (groups != null)
            {
                foreach (var g in groups)
                    g.alpha = Mathf.Lerp(1.0f, 0.0f, 1.0f);
            }
        }

        OnAfterClose();
    }
}
