using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REnemyStateResetter : DamagableStateResetter
{
    protected REnemyController _controller;
    private void Start()
    {
        _controller = GetComponent<REnemyController>();
    }
    public override void StateReset()
    {
        _controller.SetStateReset();
    }
}
