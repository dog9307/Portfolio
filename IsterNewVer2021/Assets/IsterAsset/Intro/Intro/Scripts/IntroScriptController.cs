using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroScriptController : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private Text _text;

    [TextArea(5, 10)]
    [SerializeField]
    private string _script;

    [SerializeField]
    private float _interval = 0.1f;
    [SerializeField]
    private float _fadingTime = 0.5f;
    [SerializeField]
    private float _fadingDelayTime = 1.0f;

    public bool isScriptEnd { get; set; }

    private bool _isFadingEnd = false;

    public void StartScript()
    {
        isScriptEnd = false;
        _isFadingEnd = false;
        _text.text = "";
        StartCoroutine(Scripting());
    }

    IEnumerator Scripting()
    {
        int currentLength = 0;
        while (currentLength < _script.Length)
        {
            char newChar = _script[currentLength];

            _text.text += newChar;

            if (newChar == '\n')
                yield return new WaitForSeconds(_interval);
            yield return new WaitForSeconds(_interval);

            currentLength++;
        }

        yield return new WaitForSeconds(_fadingDelayTime);

        StartCoroutine(Fading());
        while (!_isFadingEnd)
            yield return null;

        isScriptEnd = true;
    }

    IEnumerator Fading()
    {
        float currentTime = 0.0f;
        while (currentTime < _fadingTime)
        {
            float ratio = currentTime / _fadingTime;
            ApplyAlpha(1.0f - ratio);

            yield return null;

            currentTime += Time.deltaTime;
        }
        ApplyAlpha(0.0f);
        _isFadingEnd = true;
    }

    public void ApplyAlpha(float alpha)
    {
        Color color = _text.color;
        color.a = alpha;
        _text.color = color;
    }
}
