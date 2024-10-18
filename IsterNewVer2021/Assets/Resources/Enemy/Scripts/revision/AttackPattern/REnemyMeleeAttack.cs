using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MELEEATTACK
{
    NONE = -1,
    NORMAL = 0,
    DASH = 1,
    POINT = 2,
    END = 3,
}

[System.Serializable]
public class REnemyMeleeAttack : REnemyAttackPatternBase
{
    public MELEEATTACK _type;

    public float _rangeSet;

    public float _bulletScale;

    private Vector2 _attackDir;

    private PlayerMoveController _player;

    public override void Init()
    {
        base.Init();

        PatternStart();
        attackType = AP.meleebasic;

        _player = FindObjectOfType<PlayerMoveController>();
    }

    public override void Update()
    {
        if (isPatternEnd) return;

        if (_isShoot)
            PatternEnd();
        else


        base.Update();
    }
    public override void Reload()
    {
        _isShoot = false;
    }
    public override void FireBullet()
    {
        //Vector2 dir = -CommonFuncs.CalcDir(_owner._owner.Target.transform.position, _owner.transform.position);
        // Vector2 dir = CommonFuncs.CalcDir(transform.position, _player);
        //float dot = Vector3.Dot(Vector2.right, dir);
        //float startAngle = Mathf.Acos(dot);

        //if (dir.y < 0.0f)
        //    startAngle = Mathf.PI * 2 - startAngle;

        if (!_attacker)
            _attacker = GetComponent<REnemyAttacker>();

        _attackDir = _attacker.attackDir;
        float startAngle = CommonFuncs.DirToDegree(_attackDir);

        switch (_type)
        {
            case MELEEATTACK.NONE:            
                break;
            case MELEEATTACK.NORMAL:
                MeleeNormal(_bulletScale, startAngle, _attackDir);
                break;
            case MELEEATTACK.DASH:
                break;
            case MELEEATTACK.POINT:
                break;
            case MELEEATTACK.END:
                break;
            default:
                break;
        }

        //GameObject newBullet = _creator.CreateObject();
        //EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
        //
        //if (startAngle > Mathf.PI * 2)
        //    startAngle -= Mathf.PI * 2;
        //
        //newBullet.transform.position = new Vector2(newBullet.transform.position.x+dir.x *_rangeSet, newBullet.transform.position.y + dir.y * _rangeSet);
        //controller.dir = new Vector2(Mathf.Cos(startAngle), Mathf.Sin(startAngle));
        //
        //_isShoot = true;      
    }
    public void MeleeNormal(float bulletScale, float startAngle ,Vector3 dir)
    {
        GameObject newBullet = _creator.CreateObject();
        EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();

        if (startAngle > 360.0f)
            startAngle -= 360.0f;

        float angle = CommonFuncs.DirToDegree(dir);
        newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        newBullet.transform.position = new Vector2(newBullet.transform.position.x + dir.x * _rangeSet, newBullet.transform.position.y + dir.y * _rangeSet);    
        newBullet.transform.localScale = new Vector3(bulletScale, bulletScale);
        controller.dir = new Vector2(Mathf.Cos(startAngle * Mathf.Deg2Rad), Mathf.Sin(startAngle * Mathf.Deg2Rad));
        //newBullet.transform.rotation = Quaternion.LookRotation(dir);

        _isShoot = true;
    }
}
