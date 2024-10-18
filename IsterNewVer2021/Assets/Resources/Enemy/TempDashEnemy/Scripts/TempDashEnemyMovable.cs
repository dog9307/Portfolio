using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TempDashEnemyMovable : Movable
{
    [SerializeField]
    private NavMeshAgent _agent;

    private TempDashEnemyAttacker _attack;

    private PlayerMoveController _player;
    public PlayerMoveController player { get { return _player; } }
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

    void Awake()
    {
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        _attack = GetComponent<TempDashEnemyAttacker>();
    }

    protected override void ComputeVelocity()
    {
        if (_damagable.isDie)
        {
            _agent.enabled = false;
            _targetVelocity = Vector2.zero;
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
            if (_idleMoveCoroutine != null)
                StopCoroutine(_idleMoveCoroutine);

            if (_agent.enabled)
            {
                _agent.speed = speed * IsterTimeManager.enemyTimeScale;
                _agent.SetDestination(_player.center);
            }
            else
                _targetVelocity = _attack.attackVelocity * IsterTimeManager.enemyTimeScale;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _agent.stoppingDistance);
    }

    IEnumerator IdleMove()
    {
        while (true)
        {
            float x = Random.Range(-_idleMoveRange, _idleMoveRange);
            float y = Random.Range(-_idleMoveRange, _idleMoveRange);
            Vector3 offset = new Vector3(x, y, 0.0f);
            _idleMovePos.transform.position = transform.position + offset;

            if (_agent.enabled)
                _agent.SetDestination(_idleMovePos.position);

            yield return new WaitForSeconds(_idleMoveTime);
        }
    }


    public void OnHit()
    {
        _player = FindObjectOfType<PlayerMoveController>();
    }
}
