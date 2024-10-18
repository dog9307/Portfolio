using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RNaviTarget : MonoBehaviour
{
    public REnemyController _controller;

    private NavMeshAgent _agent;

    private GameObject _target;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _controller = GetComponent<REnemyController>();
        //_target = FindObjectOfType<PlayerMoveController>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!_controller._owner.Target)
        //    return;

        //if (_controller._owner.Target && _agent.isActiveAndEnabled)
        //    _agent.SetDestination(_controller._owner.Target.transform.position);

        if (_controller._owner.Target && _agent.isActiveAndEnabled)
        {
                _agent.SetDestination(_controller._owner.Target.transform.position);
            
        }


    }
}
