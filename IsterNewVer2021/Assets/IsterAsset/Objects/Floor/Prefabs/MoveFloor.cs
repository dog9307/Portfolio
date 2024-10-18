using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField]
    private Transform[] _movePoses;

    private int _currentTargetIndex;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _moveDelayTime;

    [SerializeField]
    private bool _isLoop = false;
    private bool _isReverse;

    private bool _isMove = false;

   //[SerializeField]
   //private bool _isActiveWithPlayer;
   //private bool _isActive;

    private Vector3 _prevPos;

    [SerializeField]
    GameObject _ActivateCollider;

    private Vector2 _currentDir;
    private Vector2 _additionalForce;

    void OnEnable()
    {
        foreach (var p in _movePoses)
            p.parent = null;

        _currentTargetIndex = 0;
        transform.position = _movePoses[_currentTargetIndex].position;

        _isReverse = false;
       // _isActive = false;

       // if (!_isActiveWithPlayer)
             MoveNext();
    }

    //void OnDisable()
    //{
    //    foreach (var p in _movePoses)
    //        p.parent = transform;
    //}

    void Update()
    {
        _additionalForce = _currentDir * _speed;
    }
    private void LateUpdate()
    {
        _prevPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            //if(_isActiveWithPlayer && !_isActive) 
            //    MoveNext();

            _ActivateCollider.gameObject.SetActive(false);
           
        }       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            if (_isMove)
            {
                if (!player.isHide)
                    player.additionalForce = _additionalForce;
            }
            else
                player.additionalForce = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
        if (player)
        {
            _ActivateCollider.gameObject.SetActive(true);
            player.additionalForce = Vector2.zero;
        }
    }

    void MoveNext()
    {
        _prevPos = transform.position;
        if (_isReverse)
            _currentTargetIndex--;
        else
            _currentTargetIndex++;

        if (_currentTargetIndex >= _movePoses.Length)
        {
            if (_isLoop)
                _currentTargetIndex = 0;
            else
            {
                _isReverse = true;
                _currentTargetIndex = _movePoses.Length - 2;
            }
        }
        else if (_currentTargetIndex < 0)
        {
            if (!_isLoop)
            {
                _isReverse = false;
                _currentTargetIndex = 1;
            }
        }

        StopAllCoroutines();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        // _isActive = true;

        _isMove = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = _movePoses[_currentTargetIndex].position;

        _currentDir = CommonFuncs.CalcDir(startPos, endPos);

        float currentTime = 0.0f;
        float totalTime = Vector3.Distance(startPos, endPos) / _speed;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            Vector3 newPos = Vector3.Lerp(startPos, endPos, ratio);
            transform.position = newPos;

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
        transform.position = endPos;
        _prevPos = transform.position;
        _isMove = false;

        _currentDir = Vector2.zero;

        currentTime = 0.0f;
        totalTime = _moveDelayTime;
        while (currentTime < totalTime)
        {
            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }

        MoveNext();
    }
}
