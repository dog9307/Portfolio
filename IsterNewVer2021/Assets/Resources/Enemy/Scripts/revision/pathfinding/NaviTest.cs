using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NaviTest : MonoBehaviour
{
    public NavMeshAgent _agent;

    private REnemyController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        _agent.updateUpAxis = false;
        _agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
