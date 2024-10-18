using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverDisposable : DisposableObject
{
    [HideInInspector]
    [SerializeField]
    private Animator _anim;

    [HideInInspector]
    [SerializeField]
    private Transform _stickPivot;

    [SerializeField]
    private Collider2D _col;

    public bool isAlreadyUse { get; set; } = false;

    public override void UseObject()
    {
        base.UseObject();

        _col.enabled = false;

        isAlreadyUse = true;

        _anim.SetTrigger("on");
    }

    public override void AlreadyUsed()
    {
        base.AlreadyUsed();

        _col.enabled = false;

        _anim.enabled = false;

        _stickPivot.rotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -44.21591f));
    }
}
