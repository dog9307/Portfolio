using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveOtherAttackCreator : MonoBehaviour, IObjectCreator
{
    private Movable _move;
    private NormalBulletCreator _normalCreator;

    public GameObject effectPrefab { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        _move = transform.parent.parent.GetComponentInParent<Movable>();
        _normalCreator = _move.GetComponentInChildren<NormalBulletCreator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _move.transform.position + (Vector3)(-_normalCreator.dir) * _normalCreator.distance;
    }

    public GameObject CreateObject()
    {
        GameObject newBullet = Instantiate(effectPrefab);
        newBullet.transform.position = transform.position;

        Vector2 dir = -_normalCreator.dir;
        float angle = CommonFuncs.DirToDegree(dir);
        newBullet.transform.Rotate(new Vector3(0.0f, 0.0f, angle));

        OtherShootController shoot = newBullet.GetComponentInChildren<OtherShootController>();
        if (shoot)
            shoot.dir = dir;

        return newBullet;
    }
}
