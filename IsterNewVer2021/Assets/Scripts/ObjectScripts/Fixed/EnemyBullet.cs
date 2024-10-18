using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletMovable
{  
    Vector2 bulletDir = Vector2.zero;
    public bool bombBullet=false;
    public bool meleeBullet = false;

    public GameObject _bombBullet;

    private Damager _damage;

    void Awake()
    {
        _damage = GetComponent<Damager>();
    }

    public void BasicRangedAttack(Vector3 dir)
    {
        bulletDir = dir;
        float bulletangle = Mathf.Atan2(bulletDir.y, bulletDir.x) * Mathf.Rad2Deg;
        Quaternion dirRotate = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, bulletangle +90);
        transform.rotation = Quaternion.Lerp(transform.rotation,dirRotate, 1);

        //transform.Rotate(0.0f, 0.0f, bulletangle);
        if (this) Invoke("DestroyBullet", 2.0f);
    }
    public void BomberRangedAttack(Vector3 dir)
    {
        bulletDir = dir;
        bombBullet = true;
        float bulletangle = Mathf.Atan2(bulletDir.y, bulletDir.x) * Mathf.Rad2Deg;
        Quaternion dirRotate = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, bulletangle + 90);
        transform.rotation = Quaternion.Lerp(transform.rotation, dirRotate, 1);
        //transform.Rotate(0.0f, 0.0f, bulletangle);
        if (this) Invoke("BomberBullet", 2.0f);
    }
    public void BasicMeleeAttack(Vector3 dir)
    {
        bulletDir = dir;
    }
    public void BomberMeleeAttack(Vector3 dir)
    {
        bulletDir = dir;
    }
    protected override void ComputeVelocity()
    {
        _targetVelocity = bulletDir * speed;
    }

    //public void OnTriggerEnter2D(Collider2D col)
    //{
    //    Physics.IgnoreLayerCollision(LayerMask.NameToLayer("Bullets"), LayerMask.NameToLayer("Bullets"), true);

    //    if (col.gameObject.layer == LayerMask.NameToLayer("DontMovable"))
    //    {
    //        if (_damage.owner.ToString().Equals(col.tag)) return;

    //        if (!bombBullet)
    //            DestroyBullet();
    //        else
    //            BomberBullet();
    //    }
    //}
    public void BomberMeleeBullet()
    {
        string bulletName = this.gameObject.name + "Fragments";
        //Debug.Log(bulletName);
        BulletPattern.instance.BomberBullet(bulletName, this.transform.position);
    }

    public void BomberRangeBullet()
    {
        BulletPattern.instance.BomberBullet(this.name + "Fragments", transform.position);
        Destroy(this.gameObject);
    }

    public override void DestroyBullet()
    {
        if (bombBullet)
        {
            BomberRangeBullet();
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    //public void CircleRangeAttack(PATTERNINFO patterninfo)
    //{
    //    int count = 0;
    //    int halfAttackcount = attackcount / 2;
    //    while (count < attackcount)
    //    {
    //        var bullet = ObjectPool.instance.PopFromPool(name);
    //        bullet.SetActive(true);
    //        bullet.transform.position = pos;
    //        dir.y = dir.y -( Mathf.PI / 15 * halfAttackcount) + (Mathf.PI / 15 * count);
    //        bulletDir = new Vector2(dir.x,dir.y);

    //        count++;
    //    }
    //    if (this) Invoke("DestroyBullet", 5.0f);
    //}

    //IEnumerator CircleRangeAttack(Vector3 dir,Vector3 pos, string name,float repeat, int attackcount)
    //{
    //    int currentattackcount = 0;
    //    while(true)
    //    {
    //        currentattackcount++;

    //        ObjectPool.instance.PopFromPool(name);

    //        this.transform.position = pos;
    //        this.

    //        if (currentattackcount > attackcount)
    //            break;
    //    }
    //    yield return ;
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("DontMovable"))
            DestroyBullet();
    }
}
