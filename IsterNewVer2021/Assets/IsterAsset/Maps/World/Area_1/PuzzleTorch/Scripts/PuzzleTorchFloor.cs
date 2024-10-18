using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTorchFloor : MonoBehaviour
{
    [SerializeField]
    private Collider2D[] _cols;

    [SerializeField]
    private DissolveApplier _dissolve;

    private bool _isTurnOn = false;

    void Start()
    {
        TurnOff(true);
    }

    public void Active()
    {
        _isTurnOn = !_isTurnOn;

        if (_isTurnOn)
            TurnOn();
        else
            TurnOff();
    }

    public void TurnOn()
    {
        foreach (var c in _cols)
            c.enabled = true;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(DissolveFade(1.0f));
    }

    public void TurnOff(bool isSkip = false)
    {
        foreach (var c in _cols)
            c.enabled = false;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        if (!isSkip)
            _fadeCoroutine = StartCoroutine(DissolveFade(0.0f));
        else
            _dissolve.currentFade = 0.0f;
    }

    private Coroutine _fadeCoroutine;
    IEnumerator DissolveFade(float toFade)
    {
        float startFade = _dissolve.currentFade;
        float totalTime = 0.5f;
        float currentTime = 0.0f;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            _dissolve.currentFade = Mathf.Lerp(startFade, toFade, ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        _dissolve.currentFade = toFade;
    }
}
