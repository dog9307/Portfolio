using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletCreator : MonoBehaviour, IObjectCreator
{
    [SerializeField]
    protected int _fireCount;
    public int fireCount { set { _fireCount = value; } }

    [SerializeField]
    protected GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }
    
    protected bool _isShoot;
    public bool isShoot { get { return _isShoot; } }

    //protected EnemyAttackController _attackDir;
    protected PlayerMoveController _player;

    public virtual void Start()
    {
        _isShoot = false;

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        //_attackDir = GetComponent<EnemyAttackController>();
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        newBullet.transform.position = transform.position;
        return newBullet;
    }

    public virtual void Reload()
    {
        _isShoot = false;
    }

    public virtual void RangeAttack(RANGETYPE Attacktype)
    {}

    public abstract void FireBullets();
}
