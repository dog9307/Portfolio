using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkUI : FadingUI
{
    private bool _isBlink = false;

    private Coroutine _blinkCoroutine;

    public void StartBlink()
    {
        if (_blinkCoroutine != null)
        {
            StopCoroutine(_blinkCoroutine);
            _blinkCoroutine = null;
        }

        _isBlink = true;
        _blinkCoroutine = StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (_isBlink)
        {
            show = true;
            yield return new WaitForSeconds(fadeDuration);

            if (!_isBlink)
                break;

            show = false;
            yield return new WaitForSeconds(fadeDuration);
        }
    }

    public void EndBlink()
    {
        if (_blinkCoroutine != null)
            StopCoroutine(_blinkCoroutine);

        _isBlink = false;
        show = false;
    }
}
