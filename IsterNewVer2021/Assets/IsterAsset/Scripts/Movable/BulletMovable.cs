using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletMovable : Movable
{
    [SerializeField]
    protected bool _isMoveBullet = true;
    public bool isMoveBullet { get { return _isMoveBullet; } }

    [SerializeField]
    protected bool _isCollisionDestroy;
    public bool isCollisionDestroy { get { return _isCollisionDestroy; } }

    [SerializeField]
    protected bool _isRemovable = true;
    public bool isRemovable { get { return _isRemovable; } }

    public override float speed { get { return base.speed * IsterTimeManager.enemyTimeScale; } set => base.speed = value; }
    public override bool isCanAffectedGravity { get { return _isMoveBullet && _isCanAffectedGravity; } set { _isCanAffectedGravity = value; } }

    void OnEnable()
    {
        ComponentSetting();
    }

    void Update()
    {
        _targetVelocity = Vector2.zero;
        ComputeVelocity();
        _rigid.velocity = _targetVelocity;

        if (isCanAffectedGravity)
            _rigid.velocity += additionalForce;
    }

    public abstract void DestroyBullet();

    public void CopyMovable(BulletMovable newMove)
    {
        newMove._isMoveBullet = _isMoveBullet;
        newMove._isCollisionDestroy = isCollisionDestroy;
        newMove._isRemovable = isRemovable;
        newMove._speed = _speed;
        newMove._isCanAffectedGravity = _isCanAffectedGravity;
        newMove.isTrigerringCollider = isTrigerringCollider;
        if (newMove.isTrigerringCollider)
        {
            Collider2D col = newMove.GetComponent<Collider2D>();
            col.isTrigger = true;
        }
    }
}
