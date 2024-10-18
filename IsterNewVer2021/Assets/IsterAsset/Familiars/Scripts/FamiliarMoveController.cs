using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FamiliarMoveController : Movable
{
    [SerializeField]
    protected FamiliarTargetPos _targetPos;
    
    private PlayerMoveController _player;

    public bool isArrive { get; set; }
    public bool isRelease { get; set; }

    public float playerSpeed { get { return _player.speed; } }

    public Vector2 dir
    {
        get
        {
            Vector2 dir = CommonFuncs.CalcDir(this, _targetPos);
            return dir;
        }
    }

    protected virtual void Start()
    {
        _currentTime = 0.0f;
        isArrive = false;
        isRelease = false;

        FamiliarManager fm = FindObjectOfType<FamiliarManager>();
        fm.AddFamiliar(this);

        _player = FindObjectOfType<PlayerMoveController>();
    }
    
    protected override void ComputeVelocity()
    {
        if (isRelease) return;

        Vector2 dir = Vector2.zero;
        if (!isArrive)
            dir = this.dir;

        _targetVelocity = dir * playerSpeed;
    }

    public void Release()
    {
        isRelease = true;

        FamiliarAnimController anim = GetComponent<FamiliarAnimController>();
        anim.Release();
    }
}
