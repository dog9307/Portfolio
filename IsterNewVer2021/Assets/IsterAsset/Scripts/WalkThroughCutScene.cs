using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkThroughCutScene : MonoBehaviour
{
    [SerializeField]
    private Transform _startPos;
    public Transform startPos { get { return _startPos; } set { _startPos = value; } }

    [SerializeField]
    private Transform _endPos;
    public Transform endPos { get { return _endPos; } set { _endPos = value; } }

    public Vector2 moveDir { get { return CommonFuncs.CalcDir(_startPos, _endPos); } }

    [SerializeField]
    private Transform _walkTarget;
    [SerializeField]
    private float _walkTime = 3.0f;
    public float walkTime { get { return _walkTime; } }

    private bool _isWalkStart;
    private bool _isWalkEnd;
    public bool isWalkEnd { get { return _isWalkEnd; } }

    private Coroutine _walkCoroutine;

    public UnityEvent OnCutSceneStart;
    public UnityEvent OnDuringWalk;
    public UnityEvent OnCutSceneEnd;

    void Update()
    {
        if (!(_isWalkStart && !_isWalkEnd)) return;

        if (OnDuringWalk != null)
            OnDuringWalk.Invoke();
    }

    public void StartWalk()
    {
        _isWalkStart = true;
        _isWalkEnd = false;

        _walkCoroutine = StartCoroutine(Walk(_walkTarget));
    }

    public void StartWalk(Transform walkTarget)
    {
        _isWalkStart = true;
        _isWalkEnd = false;

        _walkCoroutine = StartCoroutine(Walk(walkTarget));
    }

    IEnumerator Walk(Transform walkTarget)
    {
        if (OnCutSceneStart != null)
            OnCutSceneStart.Invoke();

        float currentTime = 0.0f;
        Vector2 newPos = Vector2.zero;
        while (currentTime < _walkTime)
        {
            float ratio = currentTime / _walkTime;
            newPos = Vector2.Lerp(_startPos.position, _endPos.position, ratio);

            walkTarget.position = newPos;

            yield return null;

            currentTime += IsterTimeManager.originDeltaTime;
        }
        walkTarget.position = _endPos.position;

        if (OnCutSceneEnd != null)
            OnCutSceneEnd.Invoke();

        _isWalkEnd = true;
    }

    public void StopInterupt()
    {
        if (_walkCoroutine == null) return;

        StopCoroutine(_walkCoroutine);
    }
}
