using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventTimer : MonoBehaviour
{
    [SerializeField]
    private float _totalTime;
    private float _currentTime;

    public float totalTime { get { return _totalTime; } set { _totalTime = value; } }
    
    public delegate void TimerEvent();
    private List<TimerEvent> _events = new List<TimerEvent>();

    public bool isEndDestroy { get; set; }

    [SerializeField]
    private bool _isAffectTimeSlow = false;

    public float leftTime { get { return (totalTime - _currentTime); } }

    void Start()
    {
        TimerStart();
    }

    public void TimerStart()
    {
        _currentTime = 0.0f;
    }

    void Update()
    {
        //if (_currentTime >= _totalTime) return;

        if (_isAffectTimeSlow)
            _currentTime += IsterTimeManager.enemyDeltaTime;
        else
            _currentTime += IsterTimeManager.deltaTime;
        if (_currentTime >= _totalTime)
            TimeEnd();
    }

    public void TimeEnd()
    {
        int count = _events.Count;
        for (int i = 0; i < count; ++i)
            _events[i]();
        _events.RemoveRange(0, count);

        if (isEndDestroy)
            Destroy(this);
    }

    public void AddEvent(TimerEvent evt)
    {
        _events.Add(evt);
    }

    public void DestroyTimer()
    {
        Destroy(this);
    }
}
