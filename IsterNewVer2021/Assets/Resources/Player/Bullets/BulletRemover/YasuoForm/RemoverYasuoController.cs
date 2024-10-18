using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoverYasuoController : RemoverController
{
    [SerializeField]
    private float _drag;

    public override void DestroyBullet()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("effectEnd");
    }

    protected override void ComputeVelocity()
    {
        _targetVelocity = dir * speed;

        _speed -= _drag * IsterTimeManager.enemyDeltaTime;
        if (_speed < 0.0f)
            _speed = 0.0f;
    }
}
