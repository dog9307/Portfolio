using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChargerFloorBulletCreator : BulletCreator
{
    public float _posX,_posY;

    public float _size;

    [SerializeField]
    private SpriteRenderer _lineDeleter;

    [SerializeField]
    private float _delay = 0.0f;

    IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(_delay);

        GameObject newBullet = CreateObject();
        newBullet.transform.localScale = (newBullet.transform.localScale / _size);
        if (_lineDeleter) _lineDeleter.enabled = false;
        newBullet.transform.position = new Vector2(transform.position.x + _posX, transform.position.y + _posY);

        //EnemyBulletController controller = newBullet.GetComponentInChildren<EnemyBulletController>();

        //if (!_isShoot)
        //{
        //    var reScale = this.transform.localScale;
        //    //newBullet.transform.localScale = reScale;
        //    //controller.gameObject.transform.position = new Vector2(transform.position.x + _posX, transform.position.y + _posY);
        //}

    }

    public override void FireBullets() // 1 = circle , 2 = semicircle
    {
        StartCoroutine(WaitForDelay());
        _isShoot = true;
    }
}
