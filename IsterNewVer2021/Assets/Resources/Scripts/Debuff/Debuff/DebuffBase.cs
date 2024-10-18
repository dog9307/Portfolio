using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DebuffBase : IBuff, IObjectCreator
{
    public GameObject effectPrefab { get; set; }
    
    public DebuffInfo owner { get; set; }

    protected float _totalTime;
    public float totalTime { get { return _totalTime; } set { _totalTime = value; } }
    protected float _currentTime;

    public float MAX_TOTAL_TIME { get; set; }

    public float figure { get; set; }

    public bool isEnd { get { return (_currentTime >= _totalTime); } }

    public virtual void Init()
    {
        effectPrefab = Resources.Load<GameObject>("Scripts/Debuff/Debuff/" + GetType().Name + "/" + GetType().Name);

        BuffOn();
    }

    public virtual void Update()
    {
        _currentTime += IsterTimeManager.enemyDeltaTime;
    }

    public void Release()
    {
        BuffOff();
    }

    public abstract void BuffOn();
    public abstract void BuffOff();

    public abstract GameObject CreateObject();

    public void AddTotalTime(float time)
    {
        _totalTime += time;
        if (_totalTime >= MAX_TOTAL_TIME)
            _totalTime = MAX_TOTAL_TIME;
    }

    public void TimeReset()
    {
        _currentTime = 0.0f;
    }
}
