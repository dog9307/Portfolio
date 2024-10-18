using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBulletController : BulletMovable
{ 
    public Vector2 dir { get; set; }
    protected Animator _anim { get; set; }

    public bool isDamaging { get { return (_rigid.velocity.magnitude >= MIN_MOVE_DISTANCE); } }

    //회전 되는 불렛이냐?
    public bool _isTurnBullet;

    public virtual void Start()
    {
        _anim = GetComponent<Animator>();

        if (_isTurnBullet)
        {
            float bulletangle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion dirRotate = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, bulletangle + 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, dirRotate, 1);
            //float bulletangle = CommonFuncs.DirToDegree(dir);
            //transform.Rotate(new Vector3(0.0f, 0.0f, bulletangle));
        }
        Invoke("DestroyBullet", 6.0f);
    }
    protected override void ComputeVelocity()
    {
        _targetVelocity = dir * speed;
    }
    public override void DestroyBullet()
    {
        _speed = 0.0f;
    }
}

