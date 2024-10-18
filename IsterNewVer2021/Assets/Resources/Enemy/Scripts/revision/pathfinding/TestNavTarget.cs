using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavTarget : MonoBehaviour
{
    private PlayerMoveController _player;

    [HideInInspector]
    public NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();
        if (!_player) return;

        _agent.SetDestination(_player.center);

        //int count = transform.childCount;
        //for (int i = 0; i < count; ++i)
        //{
        //    Transform child = transform.GetChild(i);
        //    child.rotation = Quaternion.identity;
        //}
    }
}
