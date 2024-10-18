using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BangUlYeeBulletCreator : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    protected int _fireCount;
    public int fireCount { set { _fireCount = value; } }

    [SerializeField]
    protected GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    protected bool _isShoot;
    public bool isShoot { get { return _isShoot; } }

    [SerializeField]
    private BangUlYeeAttacker _attacker;

    [SerializeField]
    private Transform _bulletShootPos;

    protected PlayerMoveController _player;
  
    public void Start()
    {
        Reload();

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _attacker = GetComponent<BangUlYeeAttacker>();
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        newBullet.transform.position = _bulletShootPos.position;
        return newBullet;
    }

    public void Reload()
    {
        _isShoot = false;
    }

    public void FireBullets()
    {
        Vector2 dir = (_player.center - _bulletShootPos.position).normalized;
        // Vector2 dir = CommonFuncs.CalcDir(transform.position, _player);
        float dot = Vector3.Dot(Vector2.right, dir);
        float startAngle = Mathf.Acos(dot);

        if (dir.y < 0.0f)
            startAngle = Mathf.PI * 2 - startAngle;
        
        for (int i = 0; i < _fireCount; ++i)
        {
            GameObject newBullet = CreateObject();
            EnemyBulletController controller = newBullet.GetComponent<EnemyBulletController>();
            if (controller)
            {
                if (i == 0)
                {
                    float angle = startAngle;

                    if (angle > Mathf.PI * 2)
                        angle -= Mathf.PI * 2;

                    controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));               
                }
                else
                {
                    {
                        float angle = startAngle - Random.Range(-60.0f, 60.0f) * Mathf.Deg2Rad;

                        if (angle > Mathf.PI * 2)
                            angle -= Mathf.PI * 2;

                        controller.dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
                    }
                }
            }
            //count++;
        }

        _isShoot = true;
    }
}
