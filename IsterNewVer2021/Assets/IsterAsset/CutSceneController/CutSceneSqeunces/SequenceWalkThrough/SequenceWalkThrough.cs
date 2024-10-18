using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SequenceWalkThrough : CutSceneSqeunceBase
{
    [Header("걷기")]
    [SerializeField]
    private Transform _startPos;
    public Transform startPos { get { return _startPos; } set { _startPos = value; } }

    [SerializeField]
    private Transform _endPos;
    public Transform endPos { get { return _endPos; } set { _endPos = value; } }

    public Vector2 moveDir { get { return CommonFuncs.CalcDir(_startPos, _endPos); } }

    [SerializeField]
    private bool _isPlayerWalk;
    [SerializeField]
    private Transform _walkTarget;

    private bool _isWalkEnd;
    public bool isWalkEnd { get { return _isWalkEnd; } }

    private Coroutine _walkCoroutine;

    public void StartWalk()
    {
        _isWalkEnd = false;

        _walkCoroutine = StartCoroutine(Walk(_walkTarget));
    }

    public void StartWalk(Transform walkTarget)
    {
        _isWalkEnd = false;

        _walkCoroutine = StartCoroutine(Walk(walkTarget));
    }

    IEnumerator Walk(Transform walkTarget)
    {
        AnimController anim = walkTarget.GetComponent<AnimController>();

        float currentTime = 0.0f;
        Vector2 newPos = Vector2.zero;
        while (currentTime < _sequenceTime)
        {
            float ratio = currentTime / _sequenceTime;
            newPos = Vector2.Lerp(_startPos.position, _endPos.position, ratio);

            walkTarget.position = newPos;

            if (anim)
                anim.CharacterSetDir(moveDir, 0.02f);

            yield return null;

            currentTime += IsterTimeManager.deltaTime;
        }
        walkTarget.position = _endPos.position;

        _isWalkEnd = true;
    }

    public void StopInterupt()
    {
        if (_walkCoroutine == null) return;

        StopCoroutine(_walkCoroutine);
        _isWalkEnd = true;
    }

    protected override IEnumerator DuringSequence()
    {
        if (_isPlayerWalk)
            _walkTarget = FindObjectOfType<PlayerMoveController>().transform;

        StartWalk(_walkTarget);
        while (!_isWalkEnd)
            yield return null;

        _isDuringSequence = false;
    }

    public void SetStartPosToPlayer()
    {
        _startPos.position = FindObjectOfType<PlayerMoveController>().transform.position;
    }

    public void SetEndPosXToPlayer()
    {
        Vector3 newPos = _endPos.position;
        newPos.x = FindObjectOfType<PlayerMoveController>().transform.position.x;
        _endPos.position = newPos;
    }

    public void SetEndPosYToPlayer()
    {
        Vector3 newPos = _endPos.position;
        newPos.y = FindObjectOfType<PlayerMoveController>().transform.position.y;
        _endPos.position = newPos;
    }
}
