using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DongleYeeMovable : Movable
{
    [SerializeField]
    private NavMeshAgent _agent;

    private DongleYeeAttacker _attack;

    private PlayerMoveController _player;
    public PlayerMoveController player { get { return _player; } }

    private float _startSpeed;

    [SerializeField]
    private Transform _idleMovePos;
    [SerializeField]
    private float _idleMoveTime = 2.0f;
    [SerializeField]
    private float _idleMoveRange = 5.0f;

    private Coroutine _idleMoveCoroutine;

    private BuffInfo _playerBuff;
    private PlayerMoveController _findingTarget;

    [SerializeField]
    private bool _isIgnorePlayerBuff = false;

    [SerializeField]
    private float _findingPlayerRange = 10.0f;
    public float findingPlayerRange
    {
        get
        {
            float findingRange = _findingPlayerRange;

            if (!_isIgnorePlayerBuff)
            {
                if (!_playerBuff)
                {
                    if (_findingTarget)
                        _playerBuff = _findingTarget.GetComponent<BuffInfo>();
                }

                if (_playerBuff)
                    findingRange *= _playerBuff.enemyVisibleRatio;
            }

            return findingRange;
        }
    }

    public void SpeedReset()
    {
        speed = _startSpeed;
    }

    void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _attack = GetComponent<DongleYeeAttacker>();

        _startSpeed = speed;

        if (_idleMovePos)
            _idleMovePos.parent = null;
    }

    protected override void ComputeVelocity()
    {
        if (_damagable.isDie)
        {
            if (_idleMoveCoroutine != null)
                StopCoroutine(_idleMoveCoroutine);

            return;
        }
        
        if (!_player)
        {
            _agent.speed = speed * IsterTimeManager.enemyTimeScale;

            if (!_findingTarget)
                _findingTarget = FindObjectOfType<PlayerMoveController>();

            if (_findingTarget)
            {
                if (_idleMoveCoroutine == null)
                    _idleMoveCoroutine = StartCoroutine(IdleMove());

                if (CommonFuncs.Distance(_findingTarget.transform.position, transform.position) < findingPlayerRange)
                    _player = _findingTarget;
            }
        }
        else
        {
            //if (_agent.enabled)
            //{
            //    if (_idleMoveCoroutine != null)
            //        StopCoroutine(_idleMoveCoroutine);

            //    _agent.speed = speed * IsterTimeManager.enemyTimeScale;
            //    // NavMeshPath path = new NavMeshPath();
            //    // _agent.CalculatePath(_player.center, path);
            //    //_agent.SetPath(path);
            //    if (_agent.remainingDistance < _attack.attackRange * 0.7f)
            //    {
            //        speed = 0;
            //    }
            //   else speed = _startSpeed;
            //}
            if (_idleMoveCoroutine != null)
                StopCoroutine(_idleMoveCoroutine);

            if (_agent.enabled)
            {
                _agent.speed = speed * IsterTimeManager.enemyTimeScale;
                _agent.SetDestination(_player.center);
            }
            else
                _targetVelocity = Vector2.zero;
        }
       // else
       // {
       //     _targetVelocity = _attack.startDir * 0 * IsterTimeManager.enemyTimeScale;
       // }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, findingPlayerRange);
    }

    IEnumerator IdleMove()
    {
        while (true)
        {
            float x = Random.Range(-_idleMoveRange, _idleMoveRange);
            float y = Random.Range(-_idleMoveRange, _idleMoveRange);
            Vector3 offset = new Vector3(x, y, 0.0f);
            _idleMovePos.transform.position = transform.position + offset;

            _agent.SetDestination(_idleMovePos.position);

            yield return new WaitForSeconds(_idleMoveTime);
        }
    }


    public void OnHit()
    {
        _player = FindObjectOfType<PlayerMoveController>();
    }
}
