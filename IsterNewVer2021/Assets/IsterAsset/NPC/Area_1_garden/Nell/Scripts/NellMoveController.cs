using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Yarn.Unity;

public class NellMoveController : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;

    [SerializeField]
    private Transform _moveDest;

    [SerializeField]
    private float _playerFollowDistance = 3.0f;
    public float playerFollowDistance { get { return _playerFollowDistance; } set { _playerFollowDistance = value; } }

    private PlayerMoveController _player;

    [SerializeField]
    private bool _isFollowPlayer = false;
    public bool isFollowPlayer { get { return _isFollowPlayer; } set { _isFollowPlayer = value; } }

    [SerializeField]
    private Transform _followTarget;
    public Transform followTarget { get { return _followTarget; } set { _followTarget = value; } }

    // Start is called before the first frame update
    void Start()
    {
        _moveDest.transform.parent = null;
        _player = FindObjectOfType<PlayerMoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_agent) return;
        if (!_agent.enabled) return;

        if (_followTarget)
        {
            _moveDest.position = _followTarget.position;
        }
        else
        {
            if (isFollowPlayer)
            {
                Vector2 dir = CommonFuncs.CalcDir(_player, this);
                _moveDest.position = _player.transform.position + (Vector3)(dir * _playerFollowDistance);
            }
        }

        _agent.SetDestination(_moveDest.position);
    }

    public void ResetFollowTarget()
    {
        _followTarget = null;
    }

    public void SetAccel(float accel)
    {
        _agent.acceleration = accel;
    }
}
