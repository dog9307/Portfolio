using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MagicArsenal;

public class SauronLaserDamager : Damager
{
    [SerializeField]
    private BoxCollider2D _hitRange;

    public void ColliderRangeRebuild(float length)
    {
        transform.rotation = Quaternion.identity;
        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));

        Vector2 offset = _hitRange.offset;
        offset.x = -length / 2.0f;
        _hitRange.offset = offset;

        Vector2 size = _hitRange.size;
        size.x = length;
        _hitRange.size = size;
    }
}
