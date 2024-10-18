using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MirageController))]
public class MirageAnimController : AnimController
{
    private MirageController _move;
    
    void Start()
    {
        _move = GetComponent<MirageController>();
    }

    void Update()
    {
        _anim.SetFloat("dirX", _move.dir.x);
        _anim.SetFloat("dirY", _move.dir.y);
    }
}
