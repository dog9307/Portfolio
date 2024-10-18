using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class REnemyTracyingRangeAttack : REnemyAttackPatternBase
{
    public RANGETYPE _type;

    private float _angleCut;

    public override void Init()
    {
        base.Init();

        attackType = AP.rangebasic;
    }

    public override void Update()
    {

        if (isPatternEnd) return;

        if (_isShoot)
            PatternEnd();

        base.Update();
    }
    public override void Reload()
    {
        _isShoot = false;
    }
    public override void FireBullet()
    {
        Vector2 dir = (_owner._owner._damagable.center - _owner.transform.position).normalized;
        // Vector2 dir = CommonFuncs.CalcDir(transform.position, _player);
        float dot = Vector3.Dot(Vector2.right, dir);
        float startAngle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            startAngle = Mathf.PI * 2 - startAngle;

        switch (_type)
        {
            case RANGETYPE.NONE:

                break;
            case RANGETYPE.SEMICIRCLE:

                _angleCut = 180 / _fireCount;
                if (_fireCount % 2 != 0)
                {
                    for (int i = 0; i < _fireCount; ++i)
                    {
                        GameObject newBullet = _creator.CreateObject();
                        EnemyTracingBulletController controller = newBullet.GetComponent<EnemyTracingBulletController>();
                        if (controller)
                        {
                            float angle = startAngle - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad;

                            if (angle > Mathf.PI * 2)
                                angle -= Mathf.PI * 2;

                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _fireCount; ++i)
                    {
                        GameObject newBullet = _creator.CreateObject();
                        EnemyTracingBulletController controller = newBullet.GetComponent<EnemyTracingBulletController>();
                        if (controller)
                        {
                            float angle = startAngle - _angleCut * (_fireCount / 2) * Mathf.Deg2Rad + _angleCut * i * Mathf.Deg2Rad;

                            if (angle > Mathf.PI * 2)
                                angle -= Mathf.PI * 2;

                            controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                        }
                    }
                }
                break;
            case RANGETYPE.CIRCLE:

                _angleCut = (Mathf.PI * 2) / _fireCount;

                for (int i = 0; i < _fireCount; ++i)
                {
                    GameObject newBullet = _creator.CreateObject();
                    EnemyTracingBulletController controller = newBullet.GetComponent<EnemyTracingBulletController>();

                    if (controller)
                    {
                        float angle = startAngle + (i * _angleCut);

                        if (angle > Mathf.PI * 2)
                            angle -= Mathf.PI * 2;

                        controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    }
                }

                _isShoot = true;
                break;
            case RANGETYPE.END:

                break;
        }
    }
}

