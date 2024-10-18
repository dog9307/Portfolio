using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum BlackMaskEventType
{
    NONE = -1,
    PRE,
    MIDDLE,
    POST,
    END
}

public class BlackMaskController : MonoBehaviour
{
    #region SINGLETON
    static private BlackMaskController _instance;
    static public BlackMaskController instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<BlackMaskController>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "BlackMaskController";
                _instance = container.AddComponent<BlackMaskController>();
            }
        }

        DontDestroyOnLoad(BlackMaskController.instance);
    }
    #endregion

    public delegate void BlackMaskEvent();

    private Queue<BlackMaskEvent> _preFading = new Queue<BlackMaskEvent>();
    private Queue<BlackMaskEvent> _middleFading = new Queue<BlackMaskEvent>();
    private Queue<BlackMaskEvent> _postFading = new Queue<BlackMaskEvent>();

    public static int BLACKMASK_ORDER_IN_LAYER = 9999;

    // 어두워지는 시간
    private float _fadeOutTime;
    // 밝아지는 시간
    private float _fadeInTime;
    private float _currentTime;

    private bool _isStart;
    private bool _isFadeOut;

    private SpriteRenderer _renderer;

    public bool isPause { get; set; }
    public bool isFading { get { return _isStart; } }

    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();

        if (!_renderer) return;
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        _renderer.sortingOrder = BLACKMASK_ORDER_IN_LAYER;

        transform.localScale = new Vector3(100.0f, 100.0f, 1.0f);

        _isStart = false;
        _isFadeOut = false;

        isPause = false;
    }
    
    public void Pause() { isPause = true; }
    public void Pause(float pauseTime) { isPause = true; Invoke("Resume", pauseTime); }
    public void Resume() { isPause = false; }

    // Update is called once per frame
    void Update()
    {
        if (!_isStart) return;
        if (isPause) return;

        float start = 0.0f;
        float end = 1.0f;
        float endTime = _fadeOutTime;
        if (!_isFadeOut)
        {
            start = 1.0f;
            end = 0.0f;

            endTime = _fadeInTime;
        }

        _currentTime += IsterTimeManager.originDeltaTime;
        float ratio = _currentTime / endTime;
        if (ratio >= 1.0f)
        {
            ratio = 1.0f;
            if (_isFadeOut)
            {
                _isFadeOut = false;
                _currentTime = 0.0f;

                ExecuteEvents(BlackMaskEventType.MIDDLE);
            }
            else
            {
                _isStart = false;
                ExecuteEvents(BlackMaskEventType.POST);

                AmbientChanger ambChanger = FindObjectOfType<AmbientChanger>();
                if (ambChanger)
                    AmbientChanger.isSkipOnce = false;

                BGMChanger bgmChanger = FindObjectOfType<BGMChanger>();
                if (bgmChanger)
                    BGMChanger.isSkipOnce = false;
            }
        }

        float alpha = Mathf.Lerp(start, end, ratio);
        ApplyAlpha(alpha);
    }

    public void AddEvent(BlackMaskEvent evt, BlackMaskEventType type = BlackMaskEventType.MIDDLE)
    {
        switch (type)
        {
            case BlackMaskEventType.PRE:
                _preFading.Enqueue(evt);
            break;

            case BlackMaskEventType.MIDDLE:
                _middleFading.Enqueue(evt);
            break;

            case BlackMaskEventType.POST:
                _postFading.Enqueue(evt);
            break;
        }
    }

    public void StartFading(float fadeOutTime = 0.5f, float fadeInTime = 0.5f)
    {
        _fadeOutTime = fadeOutTime;
        _fadeInTime = fadeInTime;
        _currentTime = 0.0f;

        _isFadeOut = true;
        _isStart = true;

        isPause = false;

        ExecuteEvents(BlackMaskEventType.PRE);
    }

    private void ExecuteEvents(BlackMaskEventType type)
    {
        Queue<BlackMaskEvent> currentQueue = null;
        switch (type)
        {
            case BlackMaskEventType.PRE:
                currentQueue = _preFading;
            break;

            case BlackMaskEventType.MIDDLE:
                currentQueue = _middleFading;

                CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
                if (brain)
                    brain.m_DefaultBlend.m_Time = 0.0f;

                if (PrefabTrashCan.instance)
                    PrefabTrashCan.instance.EmptyTrash();
            break;

            case BlackMaskEventType.POST:
                currentQueue = _postFading;
            break;
        }
        if (currentQueue == null) return;
        
        while (currentQueue.Count > 0)
        {
            BlackMaskEvent currentEvent = currentQueue.Dequeue();
            currentEvent();
        }
    }

    public void ApplyAlpha(float alpha)
    {
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
}