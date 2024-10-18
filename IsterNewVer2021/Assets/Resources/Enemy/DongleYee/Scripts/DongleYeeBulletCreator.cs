using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DongleYeeBulletCreator : MonoBehaviour, IObjectCreator
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
    private DongleYeeAttacker _attacker;

    [SerializeField]
    private Transform _bulletShootPos;

    protected PlayerMoveController _player;

    public void Start()
    {
        Reload();

        if (!_player)
            _player = FindObjectOfType<PlayerMoveController>();

        _attacker = GetComponent<DongleYeeAttacker>();
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        if (_bulletShootPos)
        {
            newBullet.transform.position = _bulletShootPos.position;
        }
        else newBullet.transform.position = transform.position;
        return newBullet;
    }

    public void Reload()
    {
        _isShoot = false;
    }

    public void FireBullets()
    {
        DongleYeeBullet();
        _isShoot = true;
    }
    public void DongleYeeBullet()
    {

        GameObject newBullet = CreateObject();
        Vector2 pos = _player.center;
        newBullet.transform.position = pos;
       // newBullet.transform.parent = transform;
    }
}
