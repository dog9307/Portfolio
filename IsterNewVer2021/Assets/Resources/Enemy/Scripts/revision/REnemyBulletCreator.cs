using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REnemyBulletCreator : MonoBehaviour , IObjectCreator
{
    [SerializeField]
    protected GameObject _prefab;
    public GameObject effectPrefab { get { return _prefab; } set { _prefab = value; } }

    [SerializeField]
    private SFXPlayer _sfx;

    [SerializeField]
    private float _bulletSizeMulti = 1.0f;

    public GameObject CreateObject()
    {
        if (_sfx)
            _sfx.PlaySFX("attack");

        GameObject newBullet = Instantiate<GameObject>(effectPrefab);
        newBullet.transform.position = transform.position;
        return newBullet;
    }
}