using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveRoomDir
{
    NONE = -1,
    Left,
    Right,
    Up,
    Down,
    END
}

public class MoveRoomBlackMask : MonoBehaviour
{
    #region SINGLETON
    static private MoveRoomBlackMask _instance;
    static public MoveRoomBlackMask instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<MoveRoomBlackMask>();
            if (!_instance)
            {
                GameObject container = new GameObject();
                container.name = "MoveRoomBlackMask";
                _instance = container.AddComponent<MoveRoomBlackMask>();
            }
        }

        DontDestroyOnLoad(MoveRoomBlackMask.instance);
    }
    #endregion

    public delegate void MoveRoomEvent();

    private Queue<MoveRoomEvent> _preFading = new Queue<MoveRoomEvent>();
    private Queue<MoveRoomEvent> _middleFading = new Queue<MoveRoomEvent>();
    private Queue<MoveRoomEvent> _postFading = new Queue<MoveRoomEvent>();

    public static int BLACKMASK_ORDER_IN_LAYER = 9999;

    // 어두워지는 시간
    [SerializeField]
    private float _fadeOutTime = 0.3f;
    [SerializeField]
    private AnimationCurve _fadeOutTimeCurve;
    // 밝아지는 시간
    [SerializeField]
    private float _fadeInTime = 0.3f;
    [SerializeField]
    private AnimationCurve _fadeInTimeCurve;

    private bool _isStart;
    private bool _isFadeOut;

    private SpriteRenderer _renderer;

    public bool isPause { get; set; }

    public MoveRoomDir from { get; set; }
    public MoveRoomDir to { get; set; }

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

    public void StartFading(float fadeOutTime = 0.5f, float fadeInTime = 0.5f)
    {
        _fadeOutTime = fadeOutTime;
        _fadeInTime = fadeInTime;

        _isFadeOut = true;
        _isStart = true;

        isPause = false;

        ExecuteEvents(BlackMaskEventType.PRE);
        StartCoroutine(GetInBlackMask());
    }

    IEnumerator GetInBlackMask()
    {
        Vector3 startPos = Vector3.zero;
        startPos.z = transform.localPosition.z;
        float scaleFactor = transform.localScale.x;
        switch (from)
        {
            case MoveRoomDir.Left:
                startPos.x = scaleFactor;
            break;

            case MoveRoomDir.Right:
                startPos.x = -scaleFactor;
            break;

            case MoveRoomDir.Up:
                startPos.y = -scaleFactor;
            break;

            case MoveRoomDir.Down:
                startPos.y = scaleFactor;
            break;
        }

        float currentTime = 0.0f;
        while (currentTime < _fadeOutTime)
        {
            float time = currentTime / _fadeOutTime;
            if (time > 1.0f)
                time = 1.0f;

            float ratio = _fadeOutTimeCurve.Evaluate(time);

            ApplyAlpha(ratio);
            transform.localPosition = Vector3.Lerp(startPos, new Vector3(0.0f, 0.0f, transform.localPosition.z), ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        currentTime = _fadeOutTime;

        ApplyAlpha(1.0f);
        transform.localPosition = Vector3.Lerp(startPos, new Vector3(0.0f, 0.0f, transform.localPosition.z), 1.0f);

        ExecuteEvents(BlackMaskEventType.MIDDLE);
        StartCoroutine(GetOutBlackMask());
    }

    IEnumerator GetOutBlackMask()
    {
        Vector3 endPos = Vector3.zero;
        endPos.z = transform.localPosition.z;
        float scaleFactor = transform.localScale.x;
        switch (to)
        {
            case MoveRoomDir.Left:
                endPos.x = scaleFactor;
            break;

            case MoveRoomDir.Right:
                endPos.x = -scaleFactor;
            break;

            case MoveRoomDir.Up:
                endPos.y = -scaleFactor;
            break;

            case MoveRoomDir.Down:
                endPos.y = scaleFactor;
            break;
        }

        float currentTime = 0.0f;
        while (currentTime < _fadeInTime)
        {
            float time = currentTime / _fadeInTime;
            if (time > 1.0f)
                time = 1.0f;

            float ratio = _fadeInTimeCurve.Evaluate(1.0f - time);

            ApplyAlpha(ratio);
            transform.localPosition = Vector3.Lerp(endPos, new Vector3(0.0f, 0.0f, transform.localPosition.z), ratio);

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        currentTime = _fadeInTime;

        ApplyAlpha(0.0f);
        transform.localPosition = Vector3.Lerp(endPos, new Vector3(0.0f, 0.0f, transform.localPosition.z), 0.0f);

        ExecuteEvents(BlackMaskEventType.POST);

        from = MoveRoomDir.NONE;
        to = MoveRoomDir.NONE;
    }

    public void AddEvent(MoveRoomEvent evt, BlackMaskEventType type = BlackMaskEventType.MIDDLE)
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

    private void ExecuteEvents(BlackMaskEventType type)
    {
        Queue<MoveRoomEvent> currentQueue = null;
        switch (type)
        {
            case BlackMaskEventType.PRE:
                currentQueue = _preFading;
            break;

            case BlackMaskEventType.MIDDLE:
                currentQueue = _middleFading;

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
            MoveRoomEvent currentEvent = currentQueue.Dequeue();
            currentEvent();
        }
    }

    public void ApplyAlpha(float alpha)
    {
        _renderer.color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }
}
