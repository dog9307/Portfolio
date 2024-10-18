using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.Settings;

public class MapInfo : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _mapName;

    [SerializeField]
    private Image _backImage;

    public delegate void ShowMapInfoEvent();
    private Queue<ShowMapInfoEvent> _postShow = new Queue<ShowMapInfoEvent>();

    [HideInInspector]
    [SerializeField]
    private LocalizeStringEvent _mapString;

    void Start()
    {
        ApplyAlpha(0.0f);
    }

    public void ShowMapInfo(Sprite back, string mapName)
    {
        _mapName.text = mapName;
        _backImage.sprite = back;

        LocalizedString localizedString = new LocalizedString("MapInfoLocalizationTable", mapName);
        _mapString.StringReference = localizedString;

        StartCoroutine(Show());
    }

    [SerializeField]
    private float _totalFadeTime = 1.0f;
    [SerializeField]
    private float _delayTime = 5.0f;
    private int _coroutineCount = 0;
    IEnumerator Show()
    {
        bool isSkip = false;
        _coroutineCount++;
        float currentTime = 0.0f;
        float ratio = 0.0f;
        float alpha = 0.0f;
        while (currentTime < _totalFadeTime)
        {
            ratio = currentTime / _totalFadeTime;

            alpha = Mathf.Lerp(0.0f, 1.0f, ratio);
            ApplyAlpha(alpha);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;

            if (_coroutineCount > 1)
            {
                isSkip = true;
                break;
            }
        }
        ApplyAlpha(1.0f);

        if (!isSkip)
        {
            currentTime = 0.0f;
            while (currentTime < _delayTime)
            {
                yield return null;

                currentTime += IsterTimeManager.originDeltaTime;

                if (_coroutineCount > 1)
                {
                    isSkip = true;
                    break;
                }
            }
        }

        if (!isSkip)
        {
            currentTime = 0.0f;
            ratio = 0.0f;
            alpha = 1.0f;
            while (currentTime < _totalFadeTime)
            {
                ratio = currentTime / _totalFadeTime;

                alpha = Mathf.Lerp(1.0f, 0.0f, ratio);
                ApplyAlpha(alpha);

                yield return null;

                currentTime += IsterTimeManager.originDeltaTime;

                if (_coroutineCount > 1)
                {
                    isSkip = true;
                    break;
                }
            }
            ApplyAlpha(0.0f);
        }

        ExecuteEvents();

        _coroutineCount--;
    }

    private void ExecuteEvents()
    {
        while (_postShow.Count > 0)
        {
            ShowMapInfoEvent currentEvent = _postShow.Dequeue();
            currentEvent();
        }
    }

    void ApplyAlpha(float alpha)
    {
        Color color;
        if (_backImage)
        {
            color = _backImage.color;
            color.a = alpha * 0.6f;
            _backImage.color = color;
        }

        if (_mapName)
        {
            color = _mapName.color;
            color.a = alpha;
            _mapName.color = color;
        }
    }

    public void AddEvent(ShowMapInfoEvent evt)
    {
        _postShow.Enqueue(evt);
    }
}
