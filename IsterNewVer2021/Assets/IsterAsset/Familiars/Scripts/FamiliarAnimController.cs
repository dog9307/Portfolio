using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarAnimController : AnimController
{
    private FamiliarMoveController _move;

    void Start()
    {
        _move = GetComponent<FamiliarMoveController>();
    }

    void Update()
    {
        float velocity = _move.targetVelocity.magnitude;

        _anim.SetFloat("dirX", _move.dir.x);
        _anim.SetFloat("dirY", _move.dir.y);
        _anim.SetFloat("velocity", velocity); throw new System.NotImplementedException();
    }

    public void Release()
    {
        _anim.SetTrigger("release");
    }
}
