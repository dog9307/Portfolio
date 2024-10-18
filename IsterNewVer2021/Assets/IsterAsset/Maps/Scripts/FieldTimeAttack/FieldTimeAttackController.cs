using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FieldTimeAttackController : MonoBehaviour
{
    [SerializeField]
    private float _totalTime = 180.0f;
    public float totalTime { get { return _totalTime; } }
    private float _currentTime = 0.0f;
    public float currentTime { get { return _currentTime; } }

    public float ratio
    {
        get
        {
            float ratio = (currentTime / totalTime);
            return (ratio > 1.0f ? 1.0f : ratio);
        }
    }

    public UnityEvent onTimeOver;
    public UnityEvent onSuccess;

    private Coroutine _timer;

    public bool isTimerOn { get { return (_timer != null); } }

    [SerializeField]
    private FieldTimeAttackSuccessCondition _condition;
    [HideInInspector]
    [SerializeField]
    private FieldTimeAttackProgressBar _progress;

    void Update()
    {
        if (_timer == null) return;

        if (_condition.IsSuccess())
            EndTimer();
    }

    public void StartTimer()
    {
        if (_timer != null) return;

        _progress.Fade(1.0f);

        _timer = StartCoroutine(Timer());
    }

    public void EndTimer()
    {
        _progress.Fade(0.0f);

        // success
        if (_timer != null)
        {
            StopCoroutine(_timer);
            _timer = null;

            Invoke(onSuccess);
        }
        // timer over
        else
            Invoke(onTimeOver);
    }

    IEnumerator Timer()
    {
        _currentTime = 0.0f;
        while (_currentTime < _totalTime)
        {
            yield return null;

            _currentTime += IsterTimeManager.enemyDeltaTime;
        }
        _currentTime = _totalTime;

        _timer = null;

        EndTimer();
    }

    void Invoke(UnityEvent dele)
    {
        if (dele != null)
            dele.Invoke();
    }
}
