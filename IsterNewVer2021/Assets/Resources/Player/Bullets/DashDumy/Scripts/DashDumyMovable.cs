using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDumyMovable : Movable
{
    void OnEnable()
    {
        ComponentSetting();

        _damagable = GetComponent<Damagable>();

        _currentTime = 0.0f;

        _rigid.bodyType = RigidbodyType2D.Static;
    }

    protected override void ComputeVelocity()
    {
    }
}
