using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovingObstacle : MonoBehaviour
{
    [SerializeField]
    private Transform _pivot;
    [SerializeField]
    private Transform[] _movePoses;

    [SerializeField]
    private Transform _returnPos;

    private int _currentTargetIndex;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _moveDelayTime;

    [SerializeField]
    private bool _isLoop = true;
    private bool _isReverse;

    [SerializeField]
    PlayerMoveController player;


    private PlayerMoveController _player;

    private Vector3 _prevDir;
    private Vector3 _playerDir;

    private bool _isMove = false;

    [SerializeField]
    GameObject[] _colliderObject;
    //private bool _isMove = false;

    //[SerializeField]
    //private bool _isActiveWithPlayer;
    //private bool _isActive;

    GameObject _ActivateCollider;

    [SerializeField]
    private SFXPlayer _sfx;
    private AudioSource _loop;
    [SerializeField]
    private float _sfxMaxDistance = 10.0f;

    void OnEnable()
    {
        foreach (var p in _movePoses)
            p.parent = null;

        _currentTargetIndex = 0;
        _pivot.position = _movePoses[_currentTargetIndex].position;

        _isReverse = false;
        // _isActive = false;

        // if (!_isActiveWithPlayer)
        MoveNext();
    
        _player = FindObjectOfType<PlayerMoveController>();

        _loop = _sfx.PlayLoopSFX("move");
    }

    private void OnDisable()
    {
        _loop.loop = false;
        _loop.volume = 0.0f;
        _loop.Stop();
        _loop = null;
    }

    //void OnDisable()
    //{
    //    foreach (var p in _movePoses)
    //        p.parent = transform;
    //}
    private float _targetVolume;
    private void Update()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        if (!_player) return;

        if (_isMove)
        {
            _playerDir = CommonFuncs.CalcDir(_pivot, _player).normalized;

            if (_prevDir.y > 0 && _playerDir.y > 0)
            {
                //_colliderObject[0].GetComponent<Collider2D>().isTrigger = true;            
                //ColliderRemover("up").SetActive(false);
                //_ActivateCollider = ColliderRemover("up"); 
                _ActivateCollider = _colliderObject[0];
                _ActivateCollider.SetActive(false);
            }
            else if (_prevDir.y < 0 && _playerDir.y < 0)
            {
                // _colliderObject[1].GetComponent<Collider2D>().isTrigger = true;
                // // ColliderRemover("down").SetActive(false);
                // //_ActivateCollider = ColliderRemover("down");
                _ActivateCollider = _colliderObject[1];
                _ActivateCollider.SetActive(false);
            }
            else if (_prevDir.x < 0 && _playerDir.x < 0)
            {
                //_colliderObject[2].GetComponent<Collider2D>().isTrigger = true;
                // // ColliderRemover("right").SetActive(false);
                // //_ActivateCollider = ColliderRemover("right");
                _ActivateCollider = _colliderObject[2];
                _ActivateCollider.SetActive(false);
            }
            else if (_prevDir.x < 0 && _playerDir.x < 0)
            {
                //_colliderObject[3].GetComponent<Collider2D>().isTrigger = true;
                // //ColliderRemover("left").SetActive(false);
                // //_ActivateCollider = ColliderRemover("left");
                _ActivateCollider = _colliderObject[3];
                _ActivateCollider.SetActive(false);
            }

            float distance = Vector2.Distance(transform.position, _player.transform.position);
            if (distance > _sfxMaxDistance)
                _loop.volume = 0.0f;
            else
            {
                float ratio = (1.0f - (distance / _sfxMaxDistance));
                _loop.volume = ratio;
            }

            _targetVolume = _loop.volume;
        }    
        else
        {
            if (_ActivateCollider)
                _ActivateCollider.SetActive(true);

            _loop.volume = 0.0f;
        }       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("PLAYER"))
        {
            _player.dashStartPos = _returnPos.position;
            PlayerFlyingChecker player = collision.GetComponent<PlayerFlyingChecker>();
            player.Falling();
        }
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
    //    if (player)
    //    {
    //        Debug.Log("hit");
    //        //if(_isActiveWithPlayer && !_isActive) 
    //        //    MoveNext();
    //    }
    //}

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
    //    if (player)
    //    {
    //        if (_isMove)
    //        {
    //            if (!player.isHide)
    //                player.additionalForce = CommonFuncs.CalcDir(_prevPos, transform.position) * _speed;
    //        }
    //        else
    //            player.additionalForce = Vector2.zero;
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    PlayerMoveController player = collision.GetComponent<PlayerMoveController>();
    //    if (player)
    //    {
    //        _ActivateCollider.gameObject.SetActive(true);
    //        player.additionalForce = Vector2.zero;
    //    }
    //}

    void MoveNext()
    {
        //_prevPos = transform.position;
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
        Vector3 startPos = _pivot.position;
        Vector3 endPos = _movePoses[_currentTargetIndex].position;
        _prevDir = CommonFuncs.CalcDir(startPos, endPos);
        float currentTime = 0.0f;
        float totalTime = Vector3.Distance(startPos, endPos) / _speed;
        while (currentTime < totalTime)
        {
            float ratio = currentTime / totalTime;

            Vector3 newPos = Vector3.Lerp(startPos, endPos, ratio);
            //_prevPos = transform.position;
            _pivot.position = newPos;

            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }
        _pivot.position = endPos;

        //_prevPos = transform.position;
        _isMove = false;

        _sfx.PlaySFX("stop", _targetVolume);

        currentTime = 0.0f;
        totalTime = _moveDelayTime;
        while (currentTime < totalTime)
        {
            yield return null;

            currentTime += IsterTimeManager.enemyDeltaTime;
        }

        MoveNext();
    }

    GameObject ColliderRemover(string dir)
    {
        
        foreach (GameObject obj in _colliderObject)
        {
            if (!obj) continue;

            if (obj.name == dir)
            {
                _ActivateCollider = obj;
            }
            else break;
        }

        return _ActivateCollider;
    }
}
