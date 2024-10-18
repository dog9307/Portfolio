using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TalkFromStayKey : TalkFrom
{
    [SerializeField]
    protected float _stayTime = 2.0f;
    protected float _currentTime = 0.0f;

    public float ratio { get { return (_currentTime / _stayTime); } }

    public virtual void ResetRatio()
    {
        _currentTime = 0.0f;
    }

    public virtual void UpdateRatio(float deltaTime)
    {
        _currentTime += deltaTime;
    }
}
