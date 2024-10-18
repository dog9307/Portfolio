using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueImageScaling : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
    }

    public void SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void TurnOn()
    {
        ScaleChange(1.0f, 1.0f);
    }

    public void TurnOn(float toScale, float toAlpha)
    {
        ScaleChange(toScale, toAlpha);
    }

    public void TurnOff()
    {
        float scale = DialogueManager.instance.minImageScale;
        float color = DialogueManager.instance.minImageColor;
        ScaleChange(scale, color);
    }

    public void TurnOff(float toScale, float toAlpha)
    {
        ScaleChange(toScale, toAlpha);
    }

    //private int _coroutineCount = 0;
    private float _currentScaleTime = 0.0f;
    private float _totalScaleTime = 0.15f;
    private float _currentScaleFactor = 1.0f;
    private float _currentColor = 1.0f;
    public void ScaleChange(float toScale, float toColor)
    {
        StopAllCoroutines();
        StartCoroutine(Scaling(toScale, toColor));
    }

    IEnumerator Scaling(float toScale, float toColor)
    {
        //_coroutineCount++;
        _currentScaleTime = _totalScaleTime - _currentScaleTime;
        float startScaleFactor = _currentScaleFactor;
        float startColor = _currentColor;
        while (_currentScaleTime < _totalScaleTime)
        {
            _currentScaleTime += IsterTimeManager.deltaTime;
            float ratio = _currentScaleTime / _totalScaleTime;

            _currentScaleFactor = Mathf.Lerp(startScaleFactor, toScale, ratio);
            ApplyScale(_currentScaleFactor);

            float colorRatio = Mathf.Lerp(startColor, toColor, ratio);
            ApplyColor(colorRatio);

            yield return null;

            //if (_coroutineCount > 1)
            //    break;
        }
        _currentScaleTime = _totalScaleTime;

        ApplyScale(toScale);
        ApplyColor(toColor);

        //_coroutineCount--;
    }

    public void ApplyScale(float scale)
    {
        _currentScaleFactor = scale;
        transform.localScale = new Vector3(_currentScaleFactor, Mathf.Abs(_currentScaleFactor), 1.0f);
    }

    public void ApplyColor(float toColor)
    {
        _currentColor = toColor;

        Color color = new Color(_currentColor, _currentColor, _currentColor, 1.0f);
        _image.color = color;
    }
}
