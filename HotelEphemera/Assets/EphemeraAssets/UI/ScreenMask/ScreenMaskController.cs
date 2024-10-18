using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Michsky.UI.Dark;

public enum MaskingEventTiming
{
    None = -1,
    Pre,
    Middle,
    Post,
    End
}

public class ScreenMaskController : MonoBehaviour
{
    #region SINGLETON
    static private ScreenMaskController _instance;
    static public ScreenMaskController instance { get { return _instance; } }

    private void Awake()
    {
        if (!_instance)
        {
            _instance = FindObjectOfType<ScreenMaskController>();
            if (!_instance)
                return;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(ScreenMaskController.instance);
    }
    #endregion

    [SerializeField]
    private UIDissolveEffect _dissolve;

    public delegate void MaskingEvent();
    private Queue<MaskingEvent> _onPreMaskingEvents = new Queue<MaskingEvent>();
    private Queue<MaskingEvent> _onMiddleMaskingEvents = new Queue<MaskingEvent>();
    private Queue<MaskingEvent> _onPostMaskingEvents = new Queue<MaskingEvent>();

    public delegate bool MaskingCondition();
    private MaskingCondition _condition;

    public bool isPause { get; set; } = false;

    public void AddMaskingEvent(MaskingEvent evt, MaskingEventTiming timing)
    {
        switch (timing)
        {
            case MaskingEventTiming.Pre:
                _onPreMaskingEvents.Enqueue(evt);
            break;

            case MaskingEventTiming.Middle:
                _onMiddleMaskingEvents.Enqueue(evt);
            break;

            case MaskingEventTiming.Post:
                _onPostMaskingEvents.Enqueue(evt);
            break;
        }
    }

    public void SetMaskingCondition(MaskingCondition con)
    {
        _condition = con;
    }

    private void InvokeEvents(Queue<MaskingEvent> evtQueue)
    {
        while (evtQueue.Count > 0)
        {
            MaskingEvent currentEvt = evtQueue.Dequeue();
            if (currentEvt != null)
                currentEvt();
        }
    }

    public void OnPreMasking()
    {
        InvokeEvents(_onPreMaskingEvents);
    }

    public void OnMiddleMasking()
    {
        InvokeEvents(_onMiddleMaskingEvents);
    }

    public void OnPostMasking()
    {
        InvokeEvents(_onPostMaskingEvents);
    }

    public void StartFade(float maskInTime = 1.5f, float maskingTime = 2.0f, float maskOutTime = 1.5f)
    {
        StartCoroutine(Masking(maskInTime, maskingTime, maskOutTime));
    }

    IEnumerator Masking(float maskInTime, float maskingTime, float maskOutTime)
    {
        OnPreMasking();

        _dissolve.gameObject.SetActive(true);
        _dissolve.location = 1;
        _dissolve.DissolveIn();
        yield return new WaitForSecondsRealtime(maskInTime - _dissolve.animationSpeed/* * transitionMultiplier*/);

        OnMiddleMasking();
        if (_condition != null)
        {
            while (!_condition())
                yield return null;
        }

        yield return new WaitForSeconds(maskingTime);

        while (isPause)
            yield return null;

        _dissolve.DissolveOut();
        yield return new WaitForSecondsRealtime(maskOutTime - _dissolve.animationSpeed);
        _dissolve.gameObject.SetActive(false);

        OnPostMasking();
    }
}